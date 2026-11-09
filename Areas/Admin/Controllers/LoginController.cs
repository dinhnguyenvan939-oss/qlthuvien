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
public class LoginController : Controller
{
    private readonly DataContext _db;

    public LoginController(DataContext db) => _db = db;

    [AllowAnonymous]
    public async Task<IActionResult> Index(string? returnUrl = null, bool adminLogin = false)
    {
        if (User.Identity?.IsAuthenticated == true) return RedirectToLandingPage();
        if (!await _db.AdminUsers.AnyAsync(x => x.Role.Ten_Vai_tro == "Admin"))
            return RedirectToAction("Setup", "Register");

        ViewBag.ReturnUrl = returnUrl;
        ViewBag.AdminLogin = adminLogin;
        return View(new LoginViewModel { AdminLogin = adminLogin });
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(LoginViewModel model, string? returnUrl = null)
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

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction(nameof(Index));
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();

    private async Task SignInAsync(AdminUser user, bool persistent)
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
            new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = persistent });
    }

    private IActionResult RedirectToLandingPage() => User.IsInRole("Admin") || User.IsInRole("Librarian")
        ? RedirectToAction("Index", "Dashboard", new { area = "Admin" })
        : RedirectToAction("Index", "Home", new { area = "" });
}
