using System.ComponentModel.DataAnnotations;

namespace qlthuvien.Areas.Admin.Models;

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
    public int Id { get; set; }

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
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class UserDeleteViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public int LoanRecordCount { get; set; }
    public string? DeleteBlockedReason { get; set; }
    public bool CanDelete => string.IsNullOrEmpty(DeleteBlockedReason);
}
