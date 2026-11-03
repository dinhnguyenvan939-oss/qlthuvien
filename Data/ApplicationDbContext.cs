using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qlthuvien.Models;
using qlthuvien.Models.Entities;

namespace qlthuvien.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Văn học", Description = "Tác phẩm văn học trong nước và quốc tế", IsActive = true },
            new Category { Id = 2, Name = "Khoa học", Description = "Sách khoa học tự nhiên và xã hội", IsActive = true },
            new Category { Id = 3, Name = "Công nghệ", Description = "Sách công nghệ, kỹ thuật và tin học", IsActive = true },
            new Category { Id = 4, Name = "Lịch sử", Description = "Sách lịch sử Việt Nam và thế giới", IsActive = true },
            new Category { Id = 5, Name = "Thiếu nhi", Description = "Sách và truyện dành cho thiếu nhi", IsActive = true }
        );

        builder.Entity<Genre>().HasData(
            new Genre { Id = 1, Name = "Tiểu thuyết", Description = "Tiểu thuyết dài tập hoặc độc lập", CategoryId = 1, IsActive = true },
            new Genre { Id = 2, Name = "Truyện ngắn", Description = "Tập truyện ngắn", CategoryId = 1, IsActive = true },
            new Genre { Id = 3, Name = "Thơ", Description = "Thơ và tuyển tập thơ", CategoryId = 1, IsActive = true },
            new Genre { Id = 4, Name = "Toán học", Description = "Sách toán học và ứng dụng", CategoryId = 2, IsActive = true },
            new Genre { Id = 5, Name = "Sinh học", Description = "Sách sinh học và khoa học sự sống", CategoryId = 2, IsActive = true },
            new Genre { Id = 6, Name = "Lập trình", Description = "Sách ngôn ngữ và kỹ thuật lập trình", CategoryId = 3, IsActive = true },
            new Genre { Id = 7, Name = "Công nghệ thông tin", Description = "Sách hệ thống và ứng dụng CNTT", CategoryId = 3, IsActive = true },
            new Genre { Id = 8, Name = "Lịch sử Việt Nam", Description = "Sách lịch sử Việt Nam", CategoryId = 4, IsActive = true },
            new Genre { Id = 9, Name = "Lịch sử thế giới", Description = "Sách lịch sử thế giới", CategoryId = 4, IsActive = true },
            new Genre { Id = 10, Name = "Truyện thiếu nhi", Description = "Truyện phù hợp với thiếu nhi", CategoryId = 5, IsActive = true }
        );

        builder.Entity<ApplicationUser>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(150).IsRequired();
            entity.HasIndex(x => x.IsActive);
        });

        builder.Entity<Category>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasIndex(x => x.Name).IsUnique();
        });

        builder.Entity<Genre>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(150).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(500);
            entity.HasIndex(x => new { x.CategoryId, x.Name }).IsUnique();
            entity.HasOne(x => x.Category).WithMany(x => x.Genres)
                .HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<Publisher>(entity =>
        {
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Address).HasMaxLength(300);
            entity.Property(x => x.Phone).HasMaxLength(30);
            entity.Property(x => x.Email).HasMaxLength(256);
        });

        builder.Entity<Author>(entity =>
        {
            entity.Property(x => x.FullName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Biography).HasMaxLength(2000);
        });

        builder.Entity<Book>(entity =>
        {
            entity.Property(x => x.ISBN).HasMaxLength(20).IsRequired();
            entity.Property(x => x.Title).HasMaxLength(250).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(2000);
            entity.HasIndex(x => x.ISBN).IsUnique();
            entity.HasOne(x => x.Genre).WithMany(x => x.Books)
                .HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.Publisher).WithMany(x => x.Books)
                .HasForeignKey(x => x.PublisherId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(x => new { x.BookId, x.AuthorId });
            entity.Property(x => x.Contribution).HasMaxLength(100);
            entity.HasOne(x => x.Book).WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.Author).WithMany(x => x.BookAuthors)
                .HasForeignKey(x => x.AuthorId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<BookCopy>(entity =>
        {
            entity.Property(x => x.Barcode).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Status).HasConversion<string>().HasMaxLength(20);
            entity.HasIndex(x => x.Barcode).IsUnique();
            entity.HasOne(x => x.Book).WithMany(x => x.Copies)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<DigitalResource>(entity =>
        {
            entity.Property(x => x.Title).HasMaxLength(250).IsRequired();
            entity.Property(x => x.FilePath).HasMaxLength(500).IsRequired();
            entity.Property(x => x.MediaType).HasMaxLength(100);
            entity.HasOne(x => x.Book).WithMany(x => x.DigitalResources)
                .HasForeignKey(x => x.BookId).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LoanRecord>(entity =>
        {
            entity.Property(x => x.Notes).HasMaxLength(1000);
            entity.HasOne(x => x.Borrower).WithMany()
                .HasForeignKey(x => x.BorrowerId).OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(x => x.ProcessedBy).WithMany()
                .HasForeignKey(x => x.ProcessedById).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<LoanItem>(entity =>
        {
            entity.Property(x => x.FineAmount).HasPrecision(10, 2);
            entity.HasOne(x => x.LoanRecord).WithMany(x => x.Items)
                .HasForeignKey(x => x.LoanRecordId).OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(x => x.BookCopy).WithMany()
                .HasForeignKey(x => x.BookCopyId).OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(x => x.BookCopyId).IsUnique()
                .HasFilter("[ReturnedAtUtc] IS NULL");
        });
    }
}

