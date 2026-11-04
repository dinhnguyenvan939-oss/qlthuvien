using System.ComponentModel.DataAnnotations;

namespace qlthuvien.Areas.Admin.Models;

public class CategoryFormViewModel
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; } = true;
}
