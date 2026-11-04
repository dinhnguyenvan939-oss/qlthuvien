using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class DashboardController : Controller
{
    private readonly DataContext _db;
    public DashboardController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.UserCount = await _db.AdminUsers.CountAsync();
        ViewBag.CategoryCount = await _db.tblDanhMuc.CountAsync();
        ViewBag.GenreCount = await _db.tblTheLoai.CountAsync();
        ViewBag.BookCount = await _db.Books.CountAsync();
        return View();
    }
}
