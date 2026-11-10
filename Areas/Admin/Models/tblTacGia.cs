using System.ComponentModel.DataAnnotations;
using qlthuvien.Models.Entities;

namespace qlthuvien.Areas.Admin.Models;

public class tblTacGia
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string FullName { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Biography { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
