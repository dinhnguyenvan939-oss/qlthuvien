using qlthuvien.Models;

namespace qlthuvien.Models.Entities;

public class LoanRecord
{
    public int Id { get; set; }
    public string BorrowerId { get; set; } = string.Empty;
    public ApplicationUser Borrower { get; set; } = null!;
    public string? ProcessedById { get; set; }
    public ApplicationUser? ProcessedBy { get; set; }
    public DateTime BorrowedAtUtc { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    public ICollection<LoanItem> Items { get; set; } = new List<LoanItem>();
}
