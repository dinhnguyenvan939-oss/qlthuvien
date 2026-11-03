using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using qlthuvien.Data;
using qlthuvien.Models;
using qlthuvien.ViewModels.Admin;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly IWebHostEnvironment _environment;

    public AccountController(UserManager<ApplicationUser> users, SignInManager<ApplicationUser> signIn, IWebHostEnvironment environment)
    {
        _users = users;
        _signIn = signIn;
        _environment = environment;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Login(string? returnUrl = null)
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

        if ((await _users.GetUsersInRoleAsync("Admin")).Count == 0)
            return RedirectToAction(nameof(Setup));

        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewBag.ReturnUrl = returnUrl;
        if (!ModelState.IsValid) return View(model);

        var user = await _users.FindByNameAsync(model.UserNameOrEmail)
            ?? await _users.FindByEmailAsync(model.UserNameOrEmail);

        if (user is null || !user.IsActive)
        {
            ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng hoặc tài khoản đã bị khóa.");
            return View(model);
        }

        var result = await _signIn.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);
        if (result.Succeeded)
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);
            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        ModelState.AddModelError(string.Empty, "Thông tin đăng nhập không đúng.");
        return View(model);
    }

    [AllowAnonymous]
    public async Task<IActionResult> Setup()
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if ((await _users.GetUsersInRoleAsync("Admin")).Count > 0)
            return RedirectToAction(nameof(Login));
        return View(new SetupAdminViewModel());
    }

    [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
    public async Task<IActionResult> Setup(SetupAdminViewModel model)
    {
        if (!_environment.IsDevelopment()) return NotFound();
        if ((await _users.GetUsersInRoleAsync("Admin")).Count > 0)
            return RedirectToAction(nameof(Login));
        if (!ModelState.IsValid) return View(model);

        var admin = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            FullName = model.FullName,
            IsActive = true
        };

        var createResult = await _users.CreateAsync(admin, model.Password);
        if (!createResult.Succeeded)
        {
            foreach (var error in createResult.Errors)
                ModelState.AddModelError(string.Empty, error.Description);
            return View(model);
        }

        var roleResult = await _users.AddToRoleAsync(admin, "Admin");
        if (!roleResult.Succeeded)
        {
            await _users.DeleteAsync(admin);
            ModelState.AddModelError(string.Empty, "Không thể cấp vai trò quản trị. Hãy chạy ứng dụng ở môi trường Development rồi thử lại.");
            return View(model);
        }

        await _signIn.SignInAsync(admin, isPersistent: false);
        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
    }

    [HttpPost, Authorize, ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction(nameof(Login));
    }

    [AllowAnonymous]
    public IActionResult AccessDenied() => View();
}
