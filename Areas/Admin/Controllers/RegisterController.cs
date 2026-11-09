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
public class RegisterController : Controller
{
    private readonly DataContext _db;
    private readonly IWebHostEnvironment _environment;

    public RegisterController(DataContext db, IWebHostEnvironment environment)
    {
        _db = db;
        _environment = environment;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Login");
        return View(new RegisterViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(RegisterViewModel model)
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
        return RedirectToAction("Index", "Login");
    }

    [AllowAnonymous]
    public async Task<IActionResult> Setup()
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if (await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin"))
            return RedirectToAction("Index", "Login");
        return View(new SetupAdminViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Setup(SetupAdminViewModel model)
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if (await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin"))
            return RedirectToAction("Index", "Login");
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
        await SignInAsync(admin);
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    private async Task SignInAsync(AdminUser user)
    {
        var roleName = await _db.Entry(user).Reference(x => x.Role).Query()
            .Select(x => x.Ten_Vai_tro).SingleAsync();
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.UserID.ToString()),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, roleName)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = false });
    }
}
