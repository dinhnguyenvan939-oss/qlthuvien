using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;
using qlthuvien.Models.Entities;
using qlthuvien.Areas.Admin.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class CategoriesController : Controller
{
    private readonly DataContext _db;
    public CategoriesController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var categories = await _db.Categories.Include(x => x.Genres)
            .OrderBy(x => x.Name).ToListAsync();
        return View(categories);
    }

    public IActionResult Create() => View(new CategoryFormViewModel());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        _db.Categories.Add(new Category
        {
            Name = model.Name.Trim(),
            Description = model.Description?.Trim(),
            IsActive = model.IsActive
        });
        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã thêm danh mục.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.Name), "Tên danh mục đã tồn tại hoặc không hợp lệ.");
            return View(model);
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var category = await _db.Categories.FindAsync(id);
        if (category is null) return NotFound();
        return View(new CategoryFormViewModel
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            IsActive = category.IsActive
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CategoryFormViewModel model)
    {
        if (!ModelState.IsValid) return View(model);
        var category = await _db.Categories.FindAsync(model.Id);
        if (category is null) return NotFound();

        category.Name = model.Name.Trim();
        category.Description = model.Description?.Trim();
        category.IsActive = model.IsActive;
        try
        {
            await _db.SaveChangesAsync();
            TempData["Success"] = "Đã cập nhật danh mục.";
            return RedirectToAction(nameof(Index));
        }
        catch (DbUpdateException)
        {
            ModelState.AddModelError(nameof(model.Name), "Tên danh mục đã tồn tại hoặc không hợp lệ.");
            return View(model);
        }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var category = await _db.Categories.Include(x => x.Genres).FirstOrDefaultAsync(x => x.Id == id);
        if (category is null) return NotFound();
        if (category.Genres.Count > 0)
        {
            TempData["Error"] = "Không thể xóa danh mục đang có thể loại. Hãy chuyển hoặc xóa thể loại trước.";
            return RedirectToAction(nameof(Index));
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa danh mục.";
        return RedirectToAction(nameof(Index));
    }
}
