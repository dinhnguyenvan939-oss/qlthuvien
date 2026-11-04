# Quản lý thư viện

Ứng dụng ASP.NET Core MVC .NET 9 dùng SQL Server và tổ chức khu vực quản trị theo project `D:\ASPNET\QLKTX\learner`.

## Cấu trúc dữ liệu

- `AdminUser` và `tblVaiTro`: tài khoản và vai trò; mật khẩu lưu dạng PBKDF2 hash.
- `AdminMenu`, `UserMenu`, `tblMenu`: menu quản trị, người dùng và giao diện public.
- Các bảng nghiệp vụ thư viện dùng tên `tbl...`: danh mục, thể loại, nhà xuất bản, tác giả, sách, bản sao, tài liệu số và mượn trả.
- ASP.NET Core cookie authentication xử lý đăng nhập; project không dùng ASP.NET Identity hoặc EF migrations.

## Chạy lần đầu

1. Mở project trong VS Code và chạy `dotnet run`.
2. Khi SQL Server có thể kết nối, `DataContext.EnsureCreated` tạo database `QuanLyThuVien` và dữ liệu mẫu.
3. Mở `/Admin/Account/Setup` ở Development để tạo tài khoản Admin đầu tiên.

Database `QLKTX` chỉ là mẫu tham khảo. Không sửa hoặc xóa database đó. Khi thay đổi schema sau này, tiếp tục theo cấu trúc `DataContext` của learner; `EnsureCreated` chỉ tạo schema cho database mới, không tự cập nhật database đã tồn tại.