using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlthuvien.Areas.Admin.Models;

[Table("AdminUser")]
public class AdminUser
{
    [Key]
    public int UserID { get; set; }

    [Required, StringLength(100)]
    public string UserName { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(256)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(500)]
    public string PasswordHash { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string FullName { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    public int RoleId { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public tblVaiTro Role { get; set; } = null!;
}