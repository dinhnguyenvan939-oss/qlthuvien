namespace qlthuvien.Models.Entities;

public class LoanItem
{
    public int Id { get; set; }
    public int LoanRecordId { get; set; }
    public LoanRecord LoanRecord { get; set; } = null!;
    public int BookCopyId { get; set; }
    public BookCopy BookCopy { get; set; } = null!;
    public DateTime DueAtUtc { get; set; }
    public DateTime? ReturnedAtUtc { get; set; }
    public decimal FineAmount { get; set; }
}
