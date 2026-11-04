# Hướng dẫn cấu trúc dự án

Khi làm sprint hoặc bổ sung chức năng, tham khảo cách tổ chức project tại `D:\ASPNET\QLKTX\learner` và giữ cách chia MVC theo khu vực nghiệp vụ. Chỉ mượn quy ước code và tổ chức thư mục; không mang bảng sinh viên, phòng ở hay dữ liệu cá nhân của QLKTX vào hệ thống thư viện.

## Quy ước thư mục

- `Areas/Admin/Controllers`: controller cho màn hình quản trị, mỗi nghiệp vụ một controller.
- `Areas/Admin/Models`: ViewModel cho form, danh sách và dữ liệu màn hình quản trị; đặt theo nhóm tính năng.
- `Areas/Admin/Views/<Feature>`: view Razor tương ứng controller.
- `Areas/Admin/Views/Shared`: layout và thành phần dùng chung trong admin.
- `Controllers` và `Views`: giao diện người đọc/public.
- `Models/Entities`: entity/domain model dùng chung cho các sprint.
- `Models/DataContext.cs`: DbContext ánh xạ trực tiếp các bảng giống learner. Khởi tạo database bằng `EnsureCreated`; không dùng migrations hoặc ASP.NET Identity.
- `Components`: ViewComponent cho menu dùng chung; menu quản trị đặt trong `Areas/Admin/Components`.
- `wwwroot/admin`: tài nguyên giao diện NiceAdmin.

QLKTX là database mẫu, chỉ đọc; tuyệt đối không xóa hoặc ghi dữ liệu vào đó. Ứng dụng này chỉ dùng database `QuanLyThuVien`. Các sprint tiếp theo tiếp tục theo nhóm `Areas/Admin/Controllers`, `Areas/Admin/Models`, `Areas/Admin/Views` và entity dùng chung dưới `Models/Entities`. Không sửa/xóa database `QLKTX`; database phát triển của ứng dụng thư viện là `QuanLyThuVien`.