using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;
using qlthuvien.ViewModels.Admin;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly RoleManager<IdentityRole> _roles;

    public UsersController(UserManager<ApplicationUser> users, RoleManager<IdentityRole> roles)
    {
        _users = users;
        _roles = roles;
    }

    public async Task<IActionResult> Index()
    {
        var result = new List<UserListItemViewModel>();
        foreach (var user in await _users.Users.OrderBy(x => x.FullName).ToListAsync())
        {
            var roles = await _users.GetRolesAsync(user);
            result.Add(new UserListItemViewModel
            {
                Id = user.Id,
                FullName = user.FullName,
                UserName = user.UserName ?? "",
                Email = user.Email ?? "",
                IsActive = user.IsActive,
                RoleName = string.Join(", ", roles)
            });
        }
        return View(result);
    }

    public async Task<IActionResult> Create()
    {
        await LoadRolesAsync();
        return View(new UserCreateViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        if (!await _roles.RoleExistsAsync(model.RoleName))
            ModelState.AddModelError(nameof(model.RoleName), "Vai trò không hợp lệ.");
        if (!ModelState.IsValid)
        {
            await LoadRolesAsync();
            return View(model);
        }

        var user = new ApplicationUser
        {
            FullName = model.FullName.Trim(),
            Email = model.Email.Trim(),
            UserName = model.UserName.Trim(),
            IsActive = true,
            EmailConfirmed = false
        };

        var created = await _users.CreateAsync(user, model.Password);
        if (!created.Succeeded)
        {
            AddErrors(created);
            await LoadRolesAsync();
            return View(model);
        }

        var assigned = await _users.AddToRoleAsync(user, model.RoleName);
        if (!assigned.Succeeded)
        {
            await _users.DeleteAsync(user);
            AddErrors(assigned);
            await LoadRolesAsync();
            return View(model);
        }

        TempData["Success"] = "Đã tạo tài khoản.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(string id)
    {
        var user = await _users.FindByIdAsync(id);
        if (user is null) return NotFound();

        var userRoles = await _users.GetRolesAsync(user);
        await LoadRolesAsync();
        return View(new UserEditViewModel
        {
            Id = user.Id,
            FullName = user.FullName,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            IsActive = user.IsActive,
            RoleName = userRoles.FirstOrDefault() ?? "Reader"
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _users.FindByIdAsync(model.Id);
        if (user is null) return NotFound();

        var isSelf = _users.GetUserId(User) == user.Id;
        var existingRoles = await _users.GetRolesAsync(user);
        if (!await _roles.RoleExistsAsync(model.RoleName))
            ModelState.AddModelError(nameof(model.RoleName), "Vai trò không hợp lệ.");

        if (isSelf && (!model.IsActive || model.RoleName != "Admin"))
            ModelState.AddModelError(string.Empty, "Bạn không thể khóa hoặc hạ vai trò của chính tài khoản đang dùng.");

        if (existingRoles.Contains("Admin") && (model.RoleName != "Admin" || !model.IsActive))
        {
            var admins = await _users.GetUsersInRoleAsync("Admin");
            var anotherActiveAdminExists = admins.Any(admin => admin.Id != user.Id && admin.IsActive);
            if (!anotherActiveAdminExists)
                ModelState.AddModelError(string.Empty, "Không thể hạ vai trò hoặc khóa Admin cuối cùng đang hoạt động.");
        }

        if (!ModelState.IsValid)
        {
            await LoadRolesAsync();
            return View(model);
        }

        user.FullName = model.FullName.Trim();
        user.Email = model.Email.Trim();
        user.UserName = model.UserName.Trim();
        user.IsActive = model.IsActive;
        user.LockoutEnabled = !model.IsActive;
        user.LockoutEnd = model.IsActive ? null : DateTimeOffset.MaxValue;

        var update = await _users.UpdateAsync(user);
        if (!update.Succeeded)
        {
            AddErrors(update);
            await LoadRolesAsync();
            return View(model);
        }

        var remove = await _users.RemoveFromRolesAsync(user, existingRoles);
        var add = remove.Succeeded ? await _users.AddToRoleAsync(user, model.RoleName) : remove;
        if (!add.Succeeded)
        {
            AddErrors(add);
            await LoadRolesAsync();
            return View(model);
        }

        await _users.UpdateSecurityStampAsync(user);
        TempData["Success"] = "Đã cập nhật tài khoản và vai trò.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRolesAsync()
    {
        ViewBag.Roles = await _roles.Roles.OrderBy(x => x.Name).Select(x => x.Name!).ToListAsync();
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
    }
}

