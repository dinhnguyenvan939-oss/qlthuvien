namespace qlthuvien.Models.Entities;

public enum BookCopyStatus
{
    Available = 0,
    OnLoan = 1,
    Lost = 2,
    Damaged = 3
}

public class BookCopy
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public string Barcode { get; set; } = string.Empty;
    public BookCopyStatus Status { get; set; } = BookCopyStatus.Available;
    public DateTime AcquiredAtUtc { get; set; } = DateTime.UtcNow;
}
