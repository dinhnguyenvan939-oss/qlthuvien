using qlthuvien.Areas.Admin.Models;

namespace qlthuvien.Models.Entities;

public class DigitalResource
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public tblSach Book { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string? MediaType { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime UploadedAtUtc { get; set; } = DateTime.UtcNow;
}
