using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;

namespace qlthuvien.Areas.Admin.Components;

public class AdminMenuViewComponent : ViewComponent
{
    private readonly DataContext _db;

    public AdminMenuViewComponent(DataContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var items = await _db.AdminMenu.Where(x => x.IsActive)
            .OrderBy(x => x.ItemOrder).ToListAsync();
        var visibleItems = items.Where(x => string.IsNullOrWhiteSpace(x.RequiredRole)
            || User.IsInRole(x.RequiredRole)).ToList();
        return View(visibleItems);
    }
}