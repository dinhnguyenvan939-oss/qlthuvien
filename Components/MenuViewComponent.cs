using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;

namespace qlthuvien.Components;

[ViewComponent(Name = "MenuView")]
public class MenuViewComponent : ViewComponent
{
    private readonly DataContext _db;

    public MenuViewComponent(DataContext db) => _db = db;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var menus = await _db.Menu
            .Where(x => x.IsActive == true && x.Postition == 1)
            .OrderBy(x => x.MenuOrder)
            .ToListAsync();
        return View("Default", menus);
    }
}