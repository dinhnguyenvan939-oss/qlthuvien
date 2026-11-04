namespace qlthuvien.Areas.Admin.Models;

public class tblDanhMuc
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<tblTheLoai> Genres { get; set; } = new List<tblTheLoai>();
}
