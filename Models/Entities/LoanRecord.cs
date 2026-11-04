using qlthuvien.Areas.Admin.Models;

namespace qlthuvien.Models.Entities;

public class LoanRecord
{
    public int Id { get; set; }
    public int BorrowerId { get; set; }
    public AdminUser Borrower { get; set; } = null!;
    public int? ProcessedById { get; set; }
    public AdminUser? ProcessedBy { get; set; }
    public DateTime BorrowedAtUtc { get; set; } = DateTime.UtcNow;
    public string? Notes { get; set; }
    public ICollection<LoanItem> Items { get; set; } = new List<LoanItem>();
}
