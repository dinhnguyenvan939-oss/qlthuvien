using System.ComponentModel.DataAnnotations;

namespace qlthuvien.ViewModels.Admin;

public class LoginViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập email hoặc tên đăng nhập.")]
    [Display(Name = "Email hoặc tên đăng nhập")]
    public string UserNameOrEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool RememberMe { get; set; }
}

public class SetupAdminViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
    [StringLength(150)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Xác nhận mật khẩu chưa khớp.")]
    [Display(Name = "Nhập lại mật khẩu")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class UserCreateViewModel
{
    [Required, StringLength(150)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 4)]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu phải có ít nhất 8 ký tự.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Vai trò")]
    public string RoleName { get; set; } = "Reader";
}

public class UserEditViewModel
{
    public string Id { get; set; } = string.Empty;

    [Required, StringLength(150)]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(100, MinimumLength = 4)]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [Display(Name = "Vai trò")]
    public string RoleName { get; set; } = "Reader";

    [Display(Name = "Tài khoản đang hoạt động")]
    public bool IsActive { get; set; }
}

public class UserListItemViewModel
{
    public string Id { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

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
