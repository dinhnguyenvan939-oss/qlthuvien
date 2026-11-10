using System.ComponentModel.DataAnnotations;

namespace qlthuvien.Areas.Admin.Models;

public class tblNXB
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    public string? Address { get; set; }

    [StringLength(30)]
    public string? Phone { get; set; }

    [StringLength(256), EmailAddress]
    public string? Email { get; set; }

    public ICollection<tblSach> Books { get; set; } = new List<tblSach>();
}