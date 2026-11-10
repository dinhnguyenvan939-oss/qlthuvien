using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class AuthorsController : Controller
{
    private readonly DataContext _db;

    public AuthorsController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var authors = await _db.tblTacGia.Include(x => x.BookAuthors)
            .OrderBy(x => x.FullName).ToListAsync();
        return View(authors);
    }

    public IActionResult Create()
    {
        ViewData["FormAction"] = nameof(Create);
        return View("Form", new AuthorFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(AuthorFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["FormAction"] = nameof(Create);
            return View("Form", model);
        }

        _db.tblTacGia.Add(new tblTacGia
        {
            FullName = model.FullName.Trim(),
            Biography = model.Biography?.Trim()
        });
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm tác giả.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var author = await _db.tblTacGia.FindAsync(id);
        if (author is null) return NotFound();

        ViewData["FormAction"] = nameof(Edit);
        return View("Form", new AuthorFormViewModel
        {
            Id = author.Id,
            FullName = author.FullName,
            Biography = author.Biography
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(AuthorFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["FormAction"] = nameof(Edit);
            return View("Form", model);
        }

        var author = await _db.tblTacGia.FindAsync(model.Id);
        if (author is null) return NotFound();

        author.FullName = model.FullName.Trim();
        author.Biography = model.Biography?.Trim();
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật tác giả.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var author = await _db.tblTacGia.Include(x => x.BookAuthors)
            .FirstOrDefaultAsync(x => x.Id == id);
        return author is null ? NotFound() : View(author);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var author = await _db.tblTacGia.Include(x => x.BookAuthors)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (author is null) return NotFound();
        if (author.BookAuthors.Count > 0)
        {
            TempData["Error"] = "Không thể xóa tác giả đang được gắn với sách.";
            return RedirectToAction(nameof(Index));
        }

        _db.tblTacGia.Remove(author);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa tác giả.";
        return RedirectToAction(nameof(Index));
    }
}
