using System.ComponentModel.DataAnnotations;

namespace qlthuvien.Areas.Admin.Models;

public class GenreFormViewModel
{
    public int Id { get; set; }

    [Required, StringLength(150)]
    [Display(Name = "Tên thể loại")]
    public string Name { get; set; } = string.Empty;

    [StringLength(500)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; } = true;
}
