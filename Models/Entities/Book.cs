namespace qlthuvien.Models.Entities;

public class Book
{
    public int Id { get; set; }
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int? PublishedYear { get; set; }
    public bool IsActive { get; set; } = true;
    public int GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
    public ICollection<BookCopy> Copies { get; set; } = new List<BookCopy>();
    public ICollection<DigitalResource> DigitalResources { get; set; } = new List<DigitalResource>();
}
