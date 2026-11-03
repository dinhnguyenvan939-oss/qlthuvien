using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Data;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _db;
    public DashboardController(ApplicationDbContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        ViewBag.UserCount = await _db.Users.CountAsync();
        ViewBag.CategoryCount = await _db.Categories.CountAsync();
        ViewBag.GenreCount = await _db.Genres.CountAsync();
        ViewBag.BookCount = await _db.Books.CountAsync();
        return View();
    }
}
