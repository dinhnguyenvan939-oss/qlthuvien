using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models;

namespace qlthuvien.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin,Librarian")]
public class PublishersController : Controller
{
    private readonly DataContext _db;

    public PublishersController(DataContext db) => _db = db;

    public async Task<IActionResult> Index()
    {
        var publishers = await _db.tblNXB.Include(x => x.Books)
            .OrderBy(x => x.Name).ToListAsync();
        return View(publishers);
    }

    public IActionResult Create()
    {
        ViewData["FormAction"] = nameof(Create);
        return View("Form", new PublisherFormViewModel());
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PublisherFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["FormAction"] = nameof(Create);
            return View("Form", model);
        }

        _db.tblNXB.Add(new tblNXB
        {
            Name = model.Name.Trim(),
            Address = model.Address?.Trim(),
            Phone = model.Phone?.Trim(),
            Email = model.Email?.Trim()
        });

        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã thêm nhà xuất bản.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var publisher = await _db.tblNXB.FindAsync(id);
        if (publisher is null) return NotFound();

        ViewData["FormAction"] = nameof(Edit);
        return View("Form", new PublisherFormViewModel
        {
            Id = publisher.Id,
            Name = publisher.Name,
            Address = publisher.Address,
            Phone = publisher.Phone,
            Email = publisher.Email
        });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(PublisherFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["FormAction"] = nameof(Edit);
            return View("Form", model);
        }

        var publisher = await _db.tblNXB.FindAsync(model.Id);
        if (publisher is null) return NotFound();

        publisher.Name = model.Name.Trim();
        publisher.Address = model.Address?.Trim();
        publisher.Phone = model.Phone?.Trim();
        publisher.Email = model.Email?.Trim();
        await _db.SaveChangesAsync();

        TempData["Success"] = "Đã cập nhật nhà xuất bản.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int id)
    {
        var publisher = await _db.tblNXB.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
        return publisher is null ? NotFound() : View(publisher);
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var publisher = await _db.tblNXB.Include(x => x.Books)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (publisher is null) return NotFound();
        if (publisher.Books.Count > 0)
        {
            TempData["Error"] = "Không thể xóa nhà xuất bản đang có sách.";
            return RedirectToAction(nameof(Index));
        }

        _db.tblNXB.Remove(publisher);
        await _db.SaveChangesAsync();
        TempData["Success"] = "Đã xóa nhà xuất bản.";
        return RedirectToAction(nameof(Index));
    }
}
