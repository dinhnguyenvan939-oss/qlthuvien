using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class BooksController : Controller
{
    private readonly DataContext _db;

    public BooksController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var books = await _db.tblSach
            .Include(x => x.Genre).ThenInclude(x => x.Category)
            .Include(x => x.Publisher)
            .Include(x => x.Copies)
            .OrderBy(x => x.Title).ToListAsync();
        return View(books);
    }

    public async Task<IActionResult> Create()
    {
        await LoadFormOptionsAsync();
        ViewData["FormAction"] = nameof(Create);
        return View("Form", new BookFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadFormOptionsAsync();
            ViewData["FormAction"] = nameof(Create);
            return View("Form", model);
        }

        _db.tblSach.Add(new tblSach
        {
            ISBN = model.ISBN.Trim(),
            Title = model.Title.Trim(),
            Description = model.Description?.Trim(),
            PublishedYear = model.PublishedYear,
            IsActive = model.IsActive,
            GenreId = model.GenreId,
            PublisherId = model.PublisherId
        });

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm sách.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.ISBN), "ISBN đã tồn tại hoặc dữ liệu liên kết không hợp lệ.");
            await LoadFormOptionsAsync();
            ViewData["FormAction"] = nameof(Create);
            return View("Form", model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var book = await _db.tblSach.FindAsync(id);
        if (book is null) return NotFound();

        await LoadFormOptionsAsync();
        ViewData["FormAction"] = nameof(Edit);
        return View("Form", new BookFormViewModel
        {
            Id = book.Id,
            ISBN = book.ISBN,
            Title = book.Title,
            Description = book.Description,
            PublishedYear = book.PublishedYear,
            IsActive = book.IsActive,
            GenreId = book.GenreId,
            PublisherId = book.PublisherId
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(BookFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadFormOptionsAsync();
            ViewData["FormAction"] = nameof(Edit);
            return View("Form", model);
        }

        var book = await _db.tblSach.FindAsync(model.Id);
        if (book is null) return NotFound();

        book.ISBN = model.ISBN.Trim();
        book.Title = model.Title.Trim();
        book.Description = model.Description?.Trim();
        book.PublishedYear = model.PublishedYear;
        book.IsActive = model.IsActive;
        book.GenreId = model.GenreId;
        book.PublisherId = model.PublisherId;

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật sách.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.ISBN), "ISBN đã tồn tại hoặc dữ liệu liên kết không hợp lệ.");
            await LoadFormOptionsAsync();
            ViewData["FormAction"] = nameof(Edit);
            return View("Form", model);
        }
    }

    public async Task<IActionResult> Delete(int id)
    {
        var book = await _db.tblSach
            .Include(x => x.Genre)
            .Include(x => x.Publisher)
            .Include(x => x.Copies)
            .Include(x => x.DigitalResources)
            .FirstOrDefaultAsync(x => x.Id == id);
        return book is null ? NotFound() : View(book);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _db.tblSach
            .Include(x => x.Copies)
            .Include(x => x.DigitalResources)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (book is null) return NotFound();
        if (book.Copies.Count > 0 || book.DigitalResources.Count > 0)
        {
            TempData["Error"] = "Không thể xóa sách đang có bản sao hoặc tài liệu số.";
            return RedirectToAction(nameof(Index));
        }

        _db.tblSach.Remove(book);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa sách.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadFormOptionsAsync()
    {
        var genres = await _db.tblTheLoai.Include(x => x.Category)
            .OrderBy(x => x.Category.Name).ThenBy(x => x.Name).ToListAsync();
        var publishers = await _db.tblNXB.OrderBy(x => x.Name).ToListAsync();
        ViewBag.Genres = new SelectList(genres, "Id", "Name");
        ViewBag.Publishers = new SelectList(publishers, "Id", "Name");
    }
}
