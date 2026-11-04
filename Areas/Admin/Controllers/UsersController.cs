using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models;
using qlthuvien.Utilities;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class UsersController : Controller
{
    private readonly DataContext _db;
    public UsersController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var users = await _db.AdminUsers.Include(x => x.Role).OrderBy(x => x.FullName).ToListAsync();
        return View(users.Select(x => new UserListItemViewModel
        {
            Id = x.UserID,
            FullName = x.FullName,
            UserName = x.UserName,
            Email = x.Email,
            RoleName = x.Role.Ten_Vai_tro,
            IsActive = x.IsActive
        }).ToList());
    }

    public async Task<IActionResult> Create()
    {
        await LoadRolesAsync();
        return View(new UserCreateViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserCreateViewModel model)
    {
        var role = await _db.tblVaiTro.SingleOrDefaultAsync(x => x.Ten_Vai_tro == model.RoleName);
        if (role is null) ModelState.AddModelError(nameof(model.RoleName), "Vai trò không hợp lệ.");
        var email = model.Email.Trim();
        var username = model.UserName.Trim();
        if (await _db.AdminUsers.AnyAsync(x => x.Email == email)) ModelState.AddModelError(nameof(model.Email), "Email đã tồn tại.");
        if (await _db.AdminUsers.AnyAsync(x => x.UserName == username)) ModelState.AddModelError(nameof(model.UserName), "Tên đăng nhập đã tồn tại.");
        if (!ModelState.IsValid)
        {
            await LoadRolesAsync();
            return View(model);
        }

        _db.AdminUsers.Add(new AdminUser
        {
            FullName = model.FullName.Trim(),
            UserName = username,
            Email = email,
            PasswordHash = PasswordService.Hash(model.Password),
            RoleId = role!.Ma_Vai_tro,
            IsActive = true
        });
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã tạo tài khoản.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var user = await _db.AdminUsers.Include(x => x.Role).SingleOrDefaultAsync(x => x.UserID == id);
        if (user is null) return NotFound();
        await LoadRolesAsync();
        return View(new UserEditViewModel
        {
            Id = user.UserID,
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            IsActive = user.IsActive,
            RoleName = user.Role.Ten_Vai_tro
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(UserEditViewModel model)
    {
        var user = await _db.AdminUsers.Include(x => x.Role).SingleOrDefaultAsync(x => x.UserID == model.Id);
        if (user is null) return NotFound();
        var role = await _db.tblVaiTro.SingleOrDefaultAsync(x => x.Ten_Vai_tro == model.RoleName);
        if (role is null) ModelState.AddModelError(nameof(model.RoleName), "Vai trò không hợp lệ.");
        var email = model.Email.Trim();
        var username = model.UserName.Trim();
        if (await _db.AdminUsers.AnyAsync(x => x.UserID != model.Id && x.Email == email)) ModelState.AddModelError(nameof(model.Email), "Email đã được sử dụng.");
        if (await _db.AdminUsers.AnyAsync(x => x.UserID != model.Id && x.UserName == username)) ModelState.AddModelError(nameof(model.UserName), "Tên đăng nhập đã được sử dụng.");

        var isSelf = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentId) && currentId == user.UserID;
        if (isSelf && (!model.IsActive || model.RoleName != "Admin"))
            ModelState.AddModelError(string.Empty, "Bạn không thể khóa hoặc hạ vai trò của tài khoản đang đăng nhập.");
        if (user.Role.Ten_Vai_tro == "Admin" && (model.RoleName != "Admin" || !model.IsActive))
        {
            var anotherAdmin = await _db.AdminUsers.AnyAsync(x => x.UserID != user.UserID && x.IsActive && x.Role.Ten_Vai_tro == "Admin");
            if (!anotherAdmin) ModelState.AddModelError(string.Empty, "Không thể hạ vai trò hoặc khóa Admin cuối cùng đang hoạt động.");
        }

        if (!ModelState.IsValid)
        {
            await LoadRolesAsync();
            return View(model);
        }
        user.FullName = model.FullName.Trim();
        user.UserName = username;
        user.Email = email;
        user.IsActive = model.IsActive;
        user.RoleId = role!.Ma_Vai_tro;
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật tài khoản và vai trò.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRolesAsync() => ViewBag.Roles = await _db.tblVaiTro.OrderBy(x => x.Ma_Vai_tro).Select(x => x.Ten_Vai_tro).ToListAsync();
}