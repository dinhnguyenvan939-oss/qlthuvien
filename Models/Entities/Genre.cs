namespace qlthuvien.Models.Entities;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}
