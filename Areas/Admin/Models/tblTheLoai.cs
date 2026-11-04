using qlthuvien.Models.Entities;

namespace qlthuvien.Areas.Admin.Models;

public class tblTheLoai
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public tblDanhMuc Category { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
