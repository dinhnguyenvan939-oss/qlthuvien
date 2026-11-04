using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models;
using qlthuvien.Utilities;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _environment;

    public AccountController(DataContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null, bool adminLogin = false)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToLandingPage();
        if (!await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin")) return RedirectToAction(nameof(Setup));
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.AdminLogin = adminLogin;
        return View(new LoginViewModel { AdminLogin = adminLogin });
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        ViewBag.AdminLogin = model.AdminLogin;
        if (!ModelState.IsValid) return View(model);
        var login = model.UserNameOrEmail.Trim();
        var user = await _db.AdminUsers.Include(x => x.Role)
            .FirstOrDefaultAsync(x => x.UserName == login || x.Email == login);
        if (user is null || !user.IsActive || !PasswordService.Verify(model.Password, user.PasswordHash))
        {
            ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng hoặc tài khoản đã bị khóa.");
            return View(model);
        }

        if (model.AdminLogin && user.Role.Ten_Vai_tro is not ("Admin" or "Librarian"))
        {
            ModelState.AddModelError(string.Empty, "Tài khoản này không có quyền truy cập khu vực quản trị.");
            return View(model);
        }

        await SignInAsync(user, model.RememberMe);
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
        return user.Role.Ten_Vai_tro is "Admin" or "Librarian"
            ? RedirectToAction("Index", "Dashboard", new { area = "Admin" })
            : RedirectToAction("Index", "Home", new { area = "" });
    }

    [AllowAnonymous]
    public IActionResult Register()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToLandingPage();
        return View(new RegisterViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var email = model.Email.Trim();
        if (await _db.AdminUsers.AnyAsync(x => x.Email == email || x.UserName == email))
        {
            ModelState.AddModelError(nameof(model.Email), "Email này đã được đăng ký.");
            return View(model);
        }

        var readerRole = await _db.tblVaiTro.SingleOrDefaultAsync(x => x.Ten_Vai_tro == "Reader");
        if (readerRole is null) return Problem("Chưa có dữ liệu vai trò. Hãy khởi tạo lại database.");
        _db.AdminUsers.Add(new AdminUser
        {
            UserName = email,
            Email = email,
            FullName = model.FullName.Trim(),
            PasswordHash = PasswordService.Hash(model.Password),
            RoleId = readerRole.Ma_Vai_tro,
            IsActive = true
        });
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đăng ký thành công. Bạn có thể đăng nhập bằng email và mật khẩu vừa tạo.";
        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    public async Task<IActionResult> Setup()
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if (await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin")) return RedirectToAction(nameof(Login));
        return View(new SetupAdminViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Setup(SetupAdminViewModel model)
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if (await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin")) return RedirectToAction(nameof(Login));
        if (!ModelState.IsValid) return View(model);
        var email = model.Email.Trim();
        if (await _db.AdminUsers.AnyAsync(x => x.Email == email || x.UserName == email))
        {
            ModelState.AddModelError(nameof(model.Email), "Email này đã được sử dụng.");
            return View(model);
        }

        var adminRole = await _db.tblVaiTro.SingleOrDefaultAsync(x => x.Ten_Vai_tro == "Admin");
        if (adminRole is null) return Problem("Chưa có dữ liệu vai trò. Hãy khởi tạo lại database.");
        var admin = new AdminUser
        {
            UserName = email,
            Email = email,
            FullName = model.FullName.Trim(),
            PasswordHash = PasswordService.Hash(model.Password),
            RoleId = adminRole.Ma_Vai_tro,
            IsActive = true
        };
        _db.AdminUsers.Add(admin);
        await _db.SaveChangesAsync();
        await SignInAsync(admin, persistent: false);
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    private async Task SignInAsync(AdminUser user, bool persistent)
    {
        var roleName = await _db.Entry(user).Reference(x => x.Role).Query().Select(x => x.Ten_Vai_tro).SingleAsync();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, roleName)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = persistent });
    }

    private IActionResult RedirectToLandingPage() => User.IsInRole("Admin") || User.IsInRole("Librarian")
        ? RedirectToAction("Index", "Dashboard", new { area = "Admin" })
        : RedirectToAction("Index", "Home", new { area = "" });
}