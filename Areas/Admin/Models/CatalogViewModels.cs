using System.ComponentModel.DataAnnotations;

namespace qlthuvien.Areas.Admin.Models;

public class PublisherFormViewModel
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Tên nhà xuất bản")]
    public string Name { get; set; } = string.Empty;

    [StringLength(300)]
    [Display(Name = "Địa chỉ")]
    public string? Address { get; set; }

    [StringLength(30)]
    [Display(Name = "Điện thoại")]
    public string? Phone { get; set; }

    [StringLength(256), EmailAddress]
    [Display(Name = "Email")]
    public string? Email { get; set; }
}

public class BookFormViewModel
{
    public int Id { get; set; }

    [Required, StringLength(20)]
    [Display(Name = "ISBN")]
    public string ISBN { get; set; } = string.Empty;

    [Required, StringLength(250)]
    [Display(Name = "Tên sách")]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Display(Name = "Năm xuất bản")]
    public int? PublishedYear { get; set; }

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; } = true;

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại.")]
    [Display(Name = "Thể loại")]
    public int GenreId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn nhà xuất bản.")]
    [Display(Name = "Nhà xuất bản")]
    public int PublisherId { get; set; }
}

public class AuthorFormViewModel
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    [Display(Name = "Họ tên tác giả")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(2000)]
    [Display(Name = "Tiểu sử")]
    public string? Biography { get; set; }
}
