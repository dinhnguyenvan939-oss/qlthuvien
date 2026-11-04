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

    public async Task<IActionResult> Delete(int id)
    {
        var user = await _db.AdminUsers.Include(x => x.Role).SingleOrDefaultAsync(x => x.UserID == id);
        if (user is null) return NotFound();

        var loanRecordCount = await _db.LoanRecords.CountAsync(x => x.BorrowerId == id || x.ProcessedById == id);
        var isCurrentUser = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentId) && currentId == id;
        var anotherAdminExists = await _db.AdminUsers.AnyAsync(x => x.UserID != id && x.Role.Ten_Vai_tro == "Admin");
        var blockedReason = isCurrentUser
            ? "Bạn không thể xóa tài khoản đang đăng nhập."
            : user.Role.Ten_Vai_tro == "Admin" && !anotherAdminExists
                ? "Không thể xóa Admin cuối cùng. Hãy tạo tài khoản Admin khác trước."
                : loanRecordCount > 0
                    ? "Tài khoản có lịch sử mượn hoặc xử lý phiếu mượn. Hãy khóa tài khoản trong trang Sửa để giữ lịch sử."
                    : null;

        return View(new UserDeleteViewModel
        {
            Id = user.UserID,
            FullName = user.FullName,
            UserName = user.UserName,
            Email = user.Email,
            RoleName = user.Role.Ten_Vai_tro,
            IsActive = user.IsActive,
            LoanRecordCount = loanRecordCount,
            DeleteBlockedReason = blockedReason
        });
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var user = await _db.AdminUsers.Include(x => x.Role).SingleOrDefaultAsync(x => x.UserID == id);
        if (user is null) return NotFound();

        var isCurrentUser = int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var currentId) && currentId == id;
        if (isCurrentUser)
        {
            TempData["Error"] = "Bạn không thể xóa tài khoản đang đăng nhập.";
            return RedirectToAction(nameof(Index));
        }

        if (user.Role.Ten_Vai_tro == "Admin"
            && !await _db.AdminUsers.AnyAsync(x => x.UserID != id && x.Role.Ten_Vai_tro == "Admin"))
        {
            TempData["Error"] = "Không thể xóa Admin cuối cùng. Hãy tạo tài khoản Admin khác trước.";
            return RedirectToAction(nameof(Index));
        }

        if (await _db.LoanRecords.AnyAsync(x => x.BorrowerId == id || x.ProcessedById == id))
        {
            TempData["Error"] = "Tài khoản có lịch sử mượn hoặc xử lý phiếu mượn. Hãy khóa tài khoản để giữ lịch sử.";
            return RedirectToAction(nameof(Index));
        }

        _db.AdminUsers.Remove(user);
        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã xóa tài khoản.";
        }
        catch (DbUpdateException)
        {
            TempData["Error"] = "Không thể xóa tài khoản vì dữ liệu khác đang tham chiếu đến tài khoản này. Hãy khóa tài khoản thay thế.";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadRolesAsync() => ViewBag.Roles = await _db.tblVaiTro.OrderBy(x => x.Ma_Vai_tro).Select(x => x.Ten_Vai_tro).ToListAsync();
}