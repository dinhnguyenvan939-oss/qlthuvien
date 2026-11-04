using Microsoft.EntityFrameworkCore;
using qlthuvien.Areas.Admin.Models;
using qlthuvien.Models.Entities;

namespace qlthuvien.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<tblVaiTro> tblVaiTro => Set<tblVaiTro>();
    public DbSet<AdminMenu> AdminMenu => Set<AdminMenu>();
    public DbSet<UserMenu> UserMenu => Set<UserMenu>();
    public DbSet<TblMenu> Menu => Set<TblMenu>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<Publisher> Publishers => Set<Publisher>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<BookAuthor> BookAuthors => Set<BookAuthor>();
    public DbSet<BookCopy> BookCopies => Set<BookCopy>();
    public DbSet<DigitalResource> DigitalResources => Set<DigitalResource>();
    public DbSet<LoanRecord> LoanRecords => Set<LoanRecord>();
    public DbSet<LoanItem> LoanItems => Set<LoanItem>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<tblVaiTro>().ToTable("tblVaiTro").HasData(
            new tblVaiTro { Ma_Vai_tro = 1, Ten_Vai_tro = "Admin" },
            new tblVaiTro { Ma_Vai_tro = 2, Ten_Vai_tro = "Librarian" },
            new tblVaiTro { Ma_Vai_tro = 3, Ten_Vai_tro = "Reader" });

        builder.Entity<AdminUser>(entity =>
        {
            entity.ToTable("AdminUser");
            entity.HasKey(x => x.UserID);
            entity.HasIndex(x => x.UserName).IsUnique();
            entity.HasIndex(x => x.Email).IsUnique();
            entity.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.Email).HasMaxLength(256).IsRequired();
            entity.Property(x => x.PasswordHash).HasMaxLength(500).IsRequired();
            entity.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            entity.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AdminMenu>().HasData(
            new AdminMenu { AdminMenuID = 1, ItemName = "Bảng điều khiển", ItemLevel = 1, ParentLevel = 0, ItemOrder = 1, IsActive = true, AreaName = "Admin", ControllerName = "Dashboard", ActionName = "Index", Icon = "bi bi-grid" },
            new AdminMenu { AdminMenuID = 2, ItemName = "Tài khoản & phân quyền", ItemLevel = 1, ParentLevel = 0, ItemOrder = 2, IsActive = true, AreaName = "Admin", ControllerName = "Users", ActionName = "Index", Icon = "bi bi-people", RequiredRole = "Admin" },
            new AdminMenu { AdminMenuID = 3, ItemName = "Quản lý danh mục sách", ItemLevel = 1, ParentLevel = 0, ItemOrder = 3, IsActive = true, Icon = "bi bi-book", IdName = "catalog-nav" },
            new AdminMenu { AdminMenuID = 4, ItemName = "Danh mục", ItemLevel = 2, ParentLevel = 3, ItemOrder = 1, IsActive = true, AreaName = "Admin", ControllerName = "Categories", ActionName = "Index", Icon = "bi bi-circle" },
            new AdminMenu { AdminMenuID = 5, ItemName = "Thể loại", ItemLevel = 2, ParentLevel = 3, ItemOrder = 2, IsActive = true, AreaName = "Admin", ControllerName = "Genres", ActionName = "Index", Icon = "bi bi-circle" });
        builder.Entity<AdminMenu>(entity =>
        {
            entity.ToTable("AdminMenu");
            entity.HasKey(x => x.AdminMenuID);
            entity.Property(x => x.ItemName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.ItemTarget).HasMaxLength(100);
            entity.Property(x => x.AreaName).HasMaxLength(50);
            entity.Property(x => x.ControllerName).HasMaxLength(50);
            entity.Property(x => x.ActionName).HasMaxLength(50);
            entity.Property(x => x.Icon).HasMaxLength(50);
            entity.Property(x => x.IdName).HasMaxLength(50);
            entity.Property(x => x.RequiredRole).HasMaxLength(100);
        });

        builder.Entity<UserMenu>().HasData(new UserMenu { UserMenuID = 1, ItemName = "Trang chủ", ItemLevel = 1, ParentLevel = 0, ItemOrder = 1, IsActive = true, ControllerName = "Home", ActionName = "Index" });
        builder.Entity<UserMenu>(entity =>
        {
            entity.ToTable("UserMenu");
            entity.HasKey(x => x.UserMenuID);
            entity.Property(x => x.ItemName).HasMaxLength(100).IsRequired();
            entity.Property(x => x.ItemTarget).HasMaxLength(100);
            entity.Property(x => x.AreaName).HasMaxLength(50);
            entity.Property(x => x.ControllerName).HasMaxLength(50);
            entity.Property(x => x.ActionName).HasMaxLength(50);
            entity.Property(x => x.Icon).HasMaxLength(50);
            entity.Property(x => x.IdName).HasMaxLength(50);
        });

        builder.Entity<TblMenu>().ToTable("tblMenu").HasData(new TblMenu { MenuID = 1, MenuName = "Trang chủ", IsActive = true, ControllerName = "Home", ActionName = "Index", Levels = 1, ParentID = 0, MenuOrder = 1, Postition = 1 });
        builder.Entity<TblMenu>(entity =>
        {
            entity.HasKey(x => x.MenuID);
            entity.Property(x => x.MenuName).HasMaxLength(50);
            entity.Property(x => x.ControllerName).HasMaxLength(50);
            entity.Property(x => x.ActionName).HasMaxLength(50);
            entity.Property(x => x.Link).HasMaxLength(50);
        });

        builder.Entity<Category>().ToTable("tblDanhMuc").HasData(
            new Category { Id = 1, Name = "Văn học", Description = "Tác phẩm văn học trong nước và quốc tế", IsActive = true },
            new Category { Id = 2, Name = "Khoa học", Description = "Sách khoa học tự nhiên và xã hội", IsActive = true },
            new Category { Id = 3, Name = "Công nghệ", Description = "Sách công nghệ, kỹ thuật và tin học", IsActive = true },
            new Category { Id = 4, Name = "Lịch sử", Description = "Sách lịch sử Việt Nam và thế giới", IsActive = true },
            new Category { Id = 5, Name = "Thiếu nhi", Description = "Sách và truyện dành cho thiếu nhi", IsActive = true });
        builder.Entity<Category>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Genre>().ToTable("tblTheLoai").HasData(
            new Genre { Id = 1, Name = "Tiểu thuyết", Description = "Tiểu thuyết dài tập hoặc độc lập", CategoryId = 1, IsActive = true },
            new Genre { Id = 2, Name = "Truyện ngắn", Description = "Tập truyện ngắn", CategoryId = 1, IsActive = true },
            new Genre { Id = 3, Name = "Thơ", Description = "Thơ và tuyển tập thơ", CategoryId = 1, IsActive = true },
            new Genre { Id = 4, Name = "Toán học", Description = "Sách toán học và ứng dụng", CategoryId = 2, IsActive = true },
            new Genre { Id = 5, Name = "Sinh học", Description = "Sách sinh học và khoa học sự sống", CategoryId = 2, IsActive = true },
            new Genre { Id = 6, Name = "Lập trình", Description = "Sách ngôn ngữ và kỹ thuật lập trình", CategoryId = 3, IsActive = true },
            new Genre { Id = 7, Name = "Công nghệ thông tin", Description = "Sách hệ thống và ứng dụng CNTT", CategoryId = 3, IsActive = true },
            new Genre { Id = 8, Name = "Lịch sử Việt Nam", Description = "Sách lịch sử Việt Nam", CategoryId = 4, IsActive = true },
            new Genre { Id = 9, Name = "Lịch sử thế giới", Description = "Sách lịch sử thế giới", CategoryId = 4, IsActive = true },
            new Genre { Id = 10, Name = "Truyện thiếu nhi", Description = "Truyện phù hợp với thiếu nhi", CategoryId = 5, IsActive = true });
        builder.Entity<Genre>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasIndex(x => new { x.CategoryId, x.Name }).IsUnique();
            entity.HasOne(x => x.Category).WithMany(x => x.Genres).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Publisher>().ToTable("tblNXB").HasData(
            new Publisher { Id = 1, Name = "Nhà xuất bản Trẻ", Address = "TP. Hồ Chí Minh", Phone = "028-39316289", Email = "info@nxbtre.com.vn" },
            new Publisher { Id = 2, Name = "Nhà xuất bản Kim Đồng", Address = "Hà Nội", Phone = "024-39434730", Email = "info@nxbkimdong.com.vn" },
            new Publisher { Id = 3, Name = "Nhà xuất bản Giáo dục Việt Nam", Address = "Hà Nội", Phone = "024-38515380", Email = "info@nxbgd.vn" });
        builder.Entity<Publisher>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(300);
            entity.Property(x => x.Phone).HasMaxLength(30);
            entity.Property(x => x.Email).HasMaxLength(256);
        });

        builder.Entity<Author>().ToTable("tblTacGia").HasData(
            new Author { Id = 1, FullName = "Nguyễn Nhật Ánh", Biography = "Nhà văn Việt Nam, tác giả nhiều tác phẩm dành cho thanh thiếu niên." },
            new Author { Id = 2, FullName = "Tô Hoài", Biography = "Nhà văn Việt Nam, tác giả Dế Mèn phiêu lưu ký." },
            new Author { Id = 3, FullName = "Nam Cao", Biography = "Nhà văn hiện thực Việt Nam." },
            new Author { Id = 4, FullName = "George Orwell", Biography = "Nhà văn và nhà báo người Anh." },
            new Author { Id = 5, FullName = "Robert C. Martin", Biography = "Tác giả và chuyên gia về phát triển phần mềm." });
        builder.Entity<Author>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Biography).HasMaxLength(2000);
        });

        builder.Entity<Book>().ToTable("tblSach").HasData(
            new Book { Id = 1, ISBN = "9786041199991", Title = "Mắt biếc", Description = "Tiểu thuyết về tình bạn và tình yêu tuổi học trò.", PublishedYear = 2019, IsActive = true, GenreId = 1, PublisherId = 1 },
            new Book { Id = 2, ISBN = "9786042230007", Title = "Dế Mèn phiêu lưu ký", Description = "Tác phẩm văn học thiếu nhi Việt Nam.", PublishedYear = 2020, IsActive = true, GenreId = 10, PublisherId = 2 },
            new Book { Id = 3, ISBN = "9786043291113", Title = "Chí Phèo", Description = "Tuyển tập truyện ngắn tiêu biểu của Nam Cao.", PublishedYear = 2021, IsActive = true, GenreId = 2, PublisherId = 3 },
            new Book { Id = 4, ISBN = "9786043452225", Title = "Clean Code", Description = "Thực hành viết mã nguồn dễ đọc và dễ bảo trì.", PublishedYear = 2022, IsActive = true, GenreId = 6, PublisherId = 1 },
            new Book { Id = 5, ISBN = "9786043563337", Title = "1984", Description = "Tiểu thuyết kinh điển về xã hội phản địa đàng.", PublishedYear = 2023, IsActive = true, GenreId = 1, PublisherId = 1 });
        builder.Entity<Book>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ISBN).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Title).HasMaxLength(250).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.HasIndex(x => x.ISBN).IsUnique();
            entity.HasOne(x => x.Genre).WithMany(x => x.Books).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Publisher).WithMany(x => x.Books).HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BookAuthor>().ToTable("tblSach_TacGia").HasData(
            new BookAuthor { BookId = 1, AuthorId = 1, Contribution = "Tác giả" },
            new BookAuthor { BookId = 2, AuthorId = 2, Contribution = "Tác giả" },
            new BookAuthor { BookId = 3, AuthorId = 3, Contribution = "Tác giả" },
            new BookAuthor { BookId = 4, AuthorId = 5, Contribution = "Tác giả" },
            new BookAuthor { BookId = 5, AuthorId = 4, Contribution = "Tác giả" });
        builder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(x => new { x.BookId, x.AuthorId });
            entity.Property(x => x.Contribution).HasMaxLength(100);
            entity.HasOne(x => x.Book).WithMany(x => x.BookAuthors).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Author).WithMany(x => x.BookAuthors).HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BookCopy>().ToTable("tblBanSaoSach").HasData(
            new BookCopy { Id = 1, BookId = 1, Barcode = "LIB-000001", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc) },
            new BookCopy { Id = 2, BookId = 1, Barcode = "LIB-000002", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 5, 0, 0, 0, DateTimeKind.Utc) },
            new BookCopy { Id = 3, BookId = 2, Barcode = "LIB-000003", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
            new BookCopy { Id = 4, BookId = 3, Barcode = "LIB-000004", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 6, 0, 0, 0, DateTimeKind.Utc) },
            new BookCopy { Id = 5, BookId = 4, Barcode = "LIB-000005", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 7, 0, 0, 0, DateTimeKind.Utc) },
            new BookCopy { Id = 6, BookId = 5, Barcode = "LIB-000006", Status = BookCopyStatus.Available, AcquiredAtUtc = new DateTime(2025, 1, 7, 0, 0, 0, DateTimeKind.Utc) });
        builder.Entity<BookCopy>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Barcode).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(x => x.Barcode).IsUnique();
            entity.HasOne(x => x.Book).WithMany(x => x.Copies).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<DigitalResource>().ToTable("tblTaiLieuSo");
        builder.Entity<DigitalResource>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(250).IsRequired();
            entity.Property(x => x.FilePath).HasMaxLength(500).IsRequired();
            entity.Property(x => x.MediaType).HasMaxLength(100);
            entity.HasOne(x => x.Book).WithMany(x => x.DigitalResources).HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LoanRecord>().ToTable("tblPhieuMuon");
        builder.Entity<LoanRecord>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Notes).HasMaxLength(1000);
            entity.HasOne(x => x.Borrower).WithMany().HasForeignKey(x => x.BorrowerId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ProcessedBy).WithMany().HasForeignKey(x => x.ProcessedById).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LoanItem>().ToTable("tblChiTietMuon");
        builder.Entity<LoanItem>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.FineAmount).HasPrecision(10, 2);
            entity.HasOne(x => x.LoanRecord).WithMany(x => x.Items).HasForeignKey(x => x.LoanRecordId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.BookCopy).WithMany().HasForeignKey(x => x.BookCopyId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.BookCopyId).IsUnique().HasFilter("[ReturnedAtUtc] IS NULL");
        });
    }
}