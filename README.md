# Quản lý thư viện

Ứng dụng quản lý thư viện xây dựng bằng ASP.NET Core MVC trên .NET 9, dùng SQL Server và giao diện quản trị NiceAdmin.

## Công nghệ và thư viện

- .NET 9 / ASP.NET Core MVC
- Entity Framework Core 9 với SQL Server (`Microsoft.EntityFrameworkCore.SqlServer`)
- SQL Server và cookie authentication
- Razor Views, Bootstrap và các tài nguyên NiceAdmin đặt trong `wwwroot`
- Cấu trúc dữ liệu ánh xạ trực tiếp qua `Models/DataContext.cs`; ứng dụng không dùng ASP.NET Identity và không dùng EF migrations.

Các thư viện NuGet được khôi phục tự động từ `qlthuvien.csproj` khi chạy `dotnet restore` hoặc `dotnet run`. Không cần cài Node.js/npm cho cấu hình hiện tại.

## Yêu cầu cài đặt

1. Windows 10/11 (hoặc môi trường hỗ trợ .NET 9).
2. .NET 9 SDK. Kiểm tra bằng:

   ```powershell
   dotnet --list-sdks
   ```

   Danh sách cần có một SDK `9.0.x`.
3. SQL Server Database Engine, ví dụ SQL Server Express hoặc LocalDB. Có thể cài SSMS để quản lý và chạy file SQL; SSMS không phải điều kiện để chạy ứng dụng nếu SQL Server đã được cài.
4. Visual Studio Code và tiện ích mở rộng C# (Microsoft) hoặc C# Dev Kit để làm việc thuận tiện.

## Lấy mã nguồn

```powershell
git clone https://github.com/dinhnguyenvan939-oss/qlthuvien.git
cd qlthuvien
```

Mở thư mục chứa `qlthuvien.csproj` trong VS Code:

```powershell
code .
```

## Cấu hình kết nối SQL Server

Mặc định, ứng dụng đọc connection string `DefaultConnection` từ `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=QuanLyThuVien;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Sửa `Server` cho đúng SQL Server trên máy của bạn, ví dụ:

- SQL Server mặc định: `Server=localhost;...`
- SQL Server Express: `Server=localhost\\SQLEXPRESS;...`
- LocalDB: `Server=(localdb)\\MSSQLLocalDB;...`

Giữ nguyên `Database=QuanLyThuVien`. Ứng dụng chỉ sử dụng database này; `QLKTX` là database mẫu tham khảo và không được sửa/xóa. Với SQL Server Authentication, thay `Trusted_Connection=True` bằng `User Id=...;Password=...;` và không commit mật khẩu vào Git. Có thể lưu cấu hình riêng trên máy bằng User Secrets:

```powershell
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=QuanLyThuVien;User Id=TEN_DANG_NHAP;Password=MAT_KHAU;TrustServerCertificate=True;"
```

## Tạo database và dữ liệu mẫu

Chọn một trong hai cách sau, không cần chạy cả hai:

### Cách 1: Để ứng dụng tự tạo database

Đảm bảo connection string trỏ đến SQL Server có thể truy cập và tài khoản kết nối có quyền tạo database. Khi chạy lần đầu, ứng dụng gọi `EnsureCreatedAsync()` để tạo schema cùng dữ liệu mẫu trong `Models/DataContext.cs`.

> `EnsureCreated` chỉ khởi tạo database chưa tồn tại. Nó không cập nhật schema cho database đã có. Khi phát triển trên database cũ, sao lưu dữ liệu trước và làm theo hướng dẫn cập nhật schema của dự án.

### Cách 2: Chạy script SQL có sẵn

1. Mở SQL Server Management Studio (SSMS) và kết nối đúng SQL Server instance đã ghi trong connection string.
2. Mở file `QuanLyThuVien.sql` ở thư mục gốc dự án.
3. Chọn **Execute** để chạy toàn bộ script. Script tạo database `QuanLyThuVien`, các bảng, khóa và dữ liệu mẫu. Không chạy trên database `QLKTX`.
4. Nếu database đã tồn tại, script có thể báo lỗi do các bảng/dữ liệu đã có; không chạy lặp lại trên database đang sử dụng.

Ứng dụng chỉ tự tạo tài khoản Admin qua `/Admin/Register/Setup` khi database chưa có tài khoản Admin. Script SQL cũng có dữ liệu tài khoản mẫu; nếu bạn dùng script, hãy đăng nhập bằng thông tin tài khoản được cung cấp riêng cho bản dự án hoặc tạo/cập nhật tài khoản theo quy trình quản trị hiện có. Không chia sẻ mật khẩu cá nhân trong repository.

## Khôi phục thư viện, build và chạy

Mở terminal tại thư mục có `qlthuvien.csproj`, sau đó chạy:

```powershell
dotnet restore
dotnet build
dotnet run
```

Theo cấu hình launch profile hiện tại, website chạy ở:

- HTTP: `http://localhost:5226`
- HTTPS: `https://localhost:7075` (nếu chứng chỉ phát triển HTTPS đã được cấu hình)

Nếu lệnh `dotnet run` không tự mở trình duyệt, truy cập một trong các địa chỉ trên.

## Đăng nhập và tài khoản

- Trang thư viện: `http://localhost:5226/`
- Đăng nhập: `http://localhost:5226/Admin/Login/Index`
- Đăng ký độc giả: `http://localhost:5226/Admin/Register/Index`
- Khởi tạo Admin lần đầu (chỉ môi trường Development, chỉ khi chưa có Admin): `http://localhost:5226/Admin/Register/Setup`

Mật khẩu được lưu dạng PBKDF2 hash. Không dùng mật khẩu mẫu hoặc thông tin đăng nhập cá nhân trên hệ thống công khai; hãy đổi thông tin mặc định trước khi triển khai.

## Cấu trúc chính

- `Areas/Admin/Controllers`, `Areas/Admin/Models`, `Areas/Admin/Views`: chức năng quản trị.
- `Controllers`, `Views`: trang dành cho người đọc.
- `Models/Entities`: các entity nghiệp vụ.
- `Models/DataContext.cs`: ánh xạ SQL Server và dữ liệu khởi tạo.
- `wwwroot`: CSS, JavaScript, hình ảnh và tài nguyên giao diện.
- `QuanLyThuVien.sql`: script tạo database và dữ liệu mẫu.

Khi bổ sung chức năng, tiếp tục theo cách tổ chức MVC của dự án và tham khảo `D:\ASPNET\QLKTX\learner` về quy ước cấu trúc. Chỉ tham khảo code/tổ chức; không thay đổi database `QLKTX`.
