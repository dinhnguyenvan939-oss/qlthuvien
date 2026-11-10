using System.ComponentModel.DataAnnotations;
using qlthuvien.Models.Entities;

namespace qlthuvien.Areas.Admin.Models;

public class tblSach
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    public string ISBN { get; set; } = string.Empty;

    [Required, StringLength(250)]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    public string? Description { get; set; }

    public int? PublishedYear { get; set; }

    public bool IsActive { get; set; } = true;

    public int GenreId { get; set; }
    public tblTheLoai Genre { get; set; } = null!;

    public int PublisherId { get; set; }
    public tblNXB Publisher { get; set; } = null!;
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
    public ICollection<DigitalResource> DigitalResources { get; set; } = new List<DigitalResource>();
}
