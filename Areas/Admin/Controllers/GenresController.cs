using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;
using qlthuvien.Areas.Admin.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class GenresController : Controller
{
    private readonly DataContext _db;
    public GenresController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var genres = await _db.tblTheLoai.Include(x => x.Category).Include(x => x.Books)
            .OrderBy(x => x.Category.Name).ThenBy(x => x.Name).ToListAsync();
        return View(genres);
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategoriesAsync();
        return View(new GenreFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GenreFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(model);
        }

        _db.tblTheLoai.Add(new tblTheLoai
        {
            Name = model.Name.Trim(),
            Description = model.Description?.Trim(),
            CategoryId = model.CategoryId,
            IsActive = model.IsActive
        });
        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm thể loại.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.Name), "Thể loại này đã tồn tại trong danh mục đã chọn.");
            await LoadCategoriesAsync();
            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var genre = await _db.tblTheLoai.FindAsync(id);
        if (genre is null) return NotFound();
        await LoadCategoriesAsync();
        return View(new GenreFormViewModel
        {
            Id = genre.Id,
            Name = genre.Name,
            Description = genre.Description,
            CategoryId = genre.CategoryId,
            IsActive = genre.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(GenreFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync();
            return View(model);
        }

        var genre = await _db.tblTheLoai.FindAsync(model.Id);
        if (genre is null) return NotFound();
        genre.Name = model.Name.Trim();
        genre.Description = model.Description?.Trim();
        genre.CategoryId = model.CategoryId;
        genre.IsActive = model.IsActive;

        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật thể loại.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.Name), "Thể loại này đã tồn tại trong danh mục đã chọn.");
            await LoadCategoriesAsync();
            return View(model);
        }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var genre = await _db.tblTheLoai.Include(x => x.Books).FirstOrDefaultAsync(x => x.Id == id);
        if (genre is null) return NotFound();
        if (genre.Books.Count > 0)
        {
            TempData["Error"] = "Không thể xóa thể loại đang được sách sử dụng.";
            return RedirectToAction(nameof(Index));
        }

        _db.tblTheLoai.Remove(genre);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa thể loại.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriesAsync()
    {
        ViewBag.Categories = new SelectList(
            await _db.tblDanhMuc.OrderBy(x => x.Name).ToListAsync(), "Id", "Name");
    }
}
