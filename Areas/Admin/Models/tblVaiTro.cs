using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlthuvien.Areas.Admin.Models;

[Table("tblVaiTro")]
public class tblVaiTro
{
    [Key]
    public int Ma_Vai_tro { get; set; }

    [Required, StringLength(50)]
    public string Ten_Vai_tro { get; set; } = string.Empty;

    public ICollection<AdminUser> Users { get; set; } = new List<AdminUser>();
}