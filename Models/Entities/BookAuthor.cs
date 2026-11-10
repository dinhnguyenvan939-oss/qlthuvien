using qlthuvien.Areas.Admin.Models;

namespace qlthuvien.Models.Entities;

public class BookAuthor
{
    public int BookId { get; set; }
    public tblSach Book { get; set; } = null!;
    public int AuthorId { get; set; }
    public tblTacGia Author { get; set; } = null!;
    public string? Contribution { get; set; }
}
