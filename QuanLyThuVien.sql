USE [master]
GO
IF DB_ID(N'QuanLyThuVien') IS NULL
    CREATE DATABASE [QuanLyThuVien]
GO
USE [QuanLyThuVien]
GO
/****** Object:  Table [dbo].[AdminMenu]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminMenu](
	[AdminMenuID] [bigint] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[ItemLevel] [int] NOT NULL,
	[ParentLevel] [int] NOT NULL,
	[ItemOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ItemTarget] [nvarchar](100) NULL,
	[AreaName] [nvarchar](50) NULL,
	[ControllerName] [nvarchar](50) NULL,
	[ActionName] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
	[IdName] [nvarchar](50) NULL,
	[RequiredRole] [nvarchar](100) NULL,
 CONSTRAINT [PK_AdminMenu] PRIMARY KEY CLUSTERED 
(
	[AdminMenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AdminUser]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AdminUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[PasswordHash] [nvarchar](500) NOT NULL,
	[FullName] [nvarchar](150) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_AdminUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblBanSaoSach]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblBanSaoSach](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[Barcode] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](20) NOT NULL,
	[AcquiredAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tblBanSaoSach] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblChiTietMuon]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblChiTietMuon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LoanRecordId] [int] NOT NULL,
	[BookCopyId] [int] NOT NULL,
	[DueAtUtc] [datetime2](7) NOT NULL,
	[ReturnedAtUtc] [datetime2](7) NULL,
	[FineAmount] [decimal](10, 2) NOT NULL,
 CONSTRAINT [PK_tblChiTietMuon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblDanhMuc]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblDanhMuc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_tblDanhMuc] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMenu]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMenu](
	[MenuID] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](50) NULL,
	[IsActive] [bit] NULL,
	[ControllerName] [nvarchar](50) NULL,
	[ActionName] [nvarchar](50) NULL,
	[Levels] [int] NOT NULL,
	[ParentID] [int] NOT NULL,
	[Link] [nvarchar](50) NULL,
	[MenuOrder] [int] NOT NULL,
	[Postition] [int] NOT NULL,
 CONSTRAINT [PK_tblMenu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblNXB]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblNXB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Phone] [nvarchar](30) NULL,
	[Email] [nvarchar](256) NULL,
 CONSTRAINT [PK_tblNXB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblPhieuMuon]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblPhieuMuon](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BorrowerId] [int] NOT NULL,
	[ProcessedById] [int] NULL,
	[BorrowedAtUtc] [datetime2](7) NOT NULL,
	[Notes] [nvarchar](1000) NULL,
 CONSTRAINT [PK_tblPhieuMuon] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSach]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSach](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ISBN] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](2000) NULL,
	[PublishedYear] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[GenreId] [int] NOT NULL,
	[PublisherId] [int] NOT NULL,
 CONSTRAINT [PK_tblSach] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblSach_TacGia]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblSach_TacGia](
	[BookId] [int] NOT NULL,
	[AuthorId] [int] NOT NULL,
	[Contribution] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblSach_TacGia] PRIMARY KEY CLUSTERED 
(
	[BookId] ASC,
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTacGia]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTacGia](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](200) NOT NULL,
	[Biography] [nvarchar](2000) NULL,
 CONSTRAINT [PK_tblTacGia] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTaiLieuSo]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTaiLieuSo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookId] [int] NOT NULL,
	[Title] [nvarchar](250) NOT NULL,
	[FilePath] [nvarchar](500) NOT NULL,
	[MediaType] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[UploadedAtUtc] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_tblTaiLieuSo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTheLoai]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTheLoai](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsActive] [bit] NOT NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_tblTheLoai] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblVaiTro]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblVaiTro](
	[Ma_Vai_tro] [int] IDENTITY(1,1) NOT NULL,
	[Ten_Vai_tro] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_tblVaiTro] PRIMARY KEY CLUSTERED 
(
	[Ma_Vai_tro] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserMenu]    Script Date: 11/4/2026 11:16:37 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMenu](
	[UserMenuID] [bigint] IDENTITY(1,1) NOT NULL,
	[ItemName] [nvarchar](100) NOT NULL,
	[ItemLevel] [int] NOT NULL,
	[ParentLevel] [int] NOT NULL,
	[ItemOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[ItemTarget] [nvarchar](100) NULL,
	[AreaName] [nvarchar](50) NULL,
	[ControllerName] [nvarchar](50) NULL,
	[ActionName] [nvarchar](50) NULL,
	[Icon] [nvarchar](50) NULL,
	[IdName] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserMenu] PRIMARY KEY CLUSTERED 
(
	[UserMenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AdminMenu] ON 

INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (1, N'Bảng điều khiển', 1, 0, 1, 1, NULL, N'Admin', N'Dashboard', N'Index', N'bi bi-grid', NULL, NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (2, N'Tài khoản & phân quyền', 1, 0, 2, 1, NULL, N'Admin', N'Users', N'Index', N'bi bi-people', NULL, N'Admin')
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (3, N'Quản lý danh mục sách', 1, 0, 3, 1, NULL, NULL, NULL, NULL, N'bi bi-book', N'catalog-nav', NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (4, N'Danh mục', 2, 3, 1, 1, NULL, N'Admin', N'Categories', N'Index', N'bi bi-circle', NULL, NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (5, N'Thể loại', 2, 3, 2, 1, NULL, N'Admin', N'Genres', N'Index', N'bi bi-circle', NULL, NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (6, N'Quản lý sách', 1, 0, 4, 1, NULL, NULL, NULL, NULL, N'bi bi-book', N'books-nav', NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (7, N'Quản lý NXB', 2, 6, 1, 1, NULL, N'Admin', N'Publishers', N'Index', N'bi bi-circle', NULL, NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (9, N'Quản lý chi tiết sách', 2, 6, 2, 1, NULL, N'Admin', N'Books', N'Index', N'bi bi-circle', NULL, NULL)
INSERT [dbo].[AdminMenu] ([AdminMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName], [RequiredRole]) VALUES (10, N'Quản lý tác giả', 2, 6, 3, 1, NULL, N'Admin', N'Authors', N'Index', N'bi bi-circle', NULL, NULL)
SET IDENTITY_INSERT [dbo].[AdminMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[AdminUser] ON 
INSERT [dbo].[AdminUser] ([UserID], [UserName], [Email], [PasswordHash], [FullName], [IsActive], [RoleId], [CreatedAtUtc]) VALUES (1, N'dinhnguyenvan939@gmail.com', N'dinhnguyenvan939@gmail.com', N'PBKDF2-SHA256$210000$p8YU1EjrZjnsMHGTQjiZ6A==$y5pqUKwqbRkrC8mWZsqlPBL+sv6TCVa3v1WdfHO9WrU=', N'admin1', 1, 1, CAST(N'2026-11-04T04:06:30.5196305' AS DateTime2))

SET IDENTITY_INSERT [dbo].[AdminUser] OFF
GO
SET IDENTITY_INSERT [dbo].[tblBanSaoSach] ON 

INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (1, 1, N'LIB-000001', N'Available', CAST(N'2025-01-05T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (2, 1, N'LIB-000002', N'Available', CAST(N'2025-01-05T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (3, 2, N'LIB-000003', N'Available', CAST(N'2025-01-06T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (4, 3, N'LIB-000004', N'Available', CAST(N'2025-01-06T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (5, 4, N'LIB-000005', N'Available', CAST(N'2025-01-07T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[tblBanSaoSach] ([Id], [BookId], [Barcode], [Status], [AcquiredAtUtc]) VALUES (6, 5, N'LIB-000006', N'Available', CAST(N'2025-01-07T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[tblBanSaoSach] OFF
GO
SET IDENTITY_INSERT [dbo].[tblDanhMuc] ON 

INSERT [dbo].[tblDanhMuc] ([Id], [Name], [Description], [IsActive]) VALUES (1, N'Văn học', N'Tác phẩm văn học trong nước và quốc tế', 1)
INSERT [dbo].[tblDanhMuc] ([Id], [Name], [Description], [IsActive]) VALUES (2, N'Khoa học', N'Sách khoa học tự nhiên và xã hội', 1)
INSERT [dbo].[tblDanhMuc] ([Id], [Name], [Description], [IsActive]) VALUES (3, N'Công nghệ', N'Sách công nghệ, kỹ thuật và tin học', 1)
INSERT [dbo].[tblDanhMuc] ([Id], [Name], [Description], [IsActive]) VALUES (4, N'Lịch sử', N'Sách lịch sử Việt Nam và thế giới', 1)
INSERT [dbo].[tblDanhMuc] ([Id], [Name], [Description], [IsActive]) VALUES (5, N'Thiếu nhi', N'Sách và truyện dành cho thiếu nhi', 1)
SET IDENTITY_INSERT [dbo].[tblDanhMuc] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMenu] ON 

INSERT [dbo].[tblMenu] ([MenuID], [MenuName], [IsActive], [ControllerName], [ActionName], [Levels], [ParentID], [Link], [MenuOrder], [Postition]) VALUES (1, N'Trang chủ', 1, N'Home', N'Index', 1, 0, NULL, 1, 1)
SET IDENTITY_INSERT [dbo].[tblMenu] OFF
GO
SET IDENTITY_INSERT [dbo].[tblNXB] ON 

INSERT [dbo].[tblNXB] ([Id], [Name], [Address], [Phone], [Email]) VALUES (1, N'Nhà xuất bản Trẻ', N'TP. Hồ Chí Minh', N'028-39316289', N'info@nxbtre.com.vn')
INSERT [dbo].[tblNXB] ([Id], [Name], [Address], [Phone], [Email]) VALUES (2, N'Nhà xuất bản Kim Đồng', N'Hà Nội', N'024-39434730', N'info@nxbkimdong.com.vn')
INSERT [dbo].[tblNXB] ([Id], [Name], [Address], [Phone], [Email]) VALUES (3, N'Nhà xuất bản Giáo dục Việt Nam', N'Hà Nội', N'024-38515380', N'info@nxbgd.vn')
SET IDENTITY_INSERT [dbo].[tblNXB] OFF
GO
SET IDENTITY_INSERT [dbo].[tblSach] ON 

INSERT [dbo].[tblSach] ([Id], [ISBN], [Title], [Description], [PublishedYear], [IsActive], [GenreId], [PublisherId]) VALUES (1, N'9786041199991', N'Mắt biếc', N'Tiểu thuyết về tình bạn và tình yêu tuổi học trò.', 2019, 1, 1, 1)
INSERT [dbo].[tblSach] ([Id], [ISBN], [Title], [Description], [PublishedYear], [IsActive], [GenreId], [PublisherId]) VALUES (2, N'9786042230007', N'Dế Mèn phiêu lưu ký', N'Tác phẩm văn học thiếu nhi Việt Nam.', 2020, 1, 10, 2)
INSERT [dbo].[tblSach] ([Id], [ISBN], [Title], [Description], [PublishedYear], [IsActive], [GenreId], [PublisherId]) VALUES (3, N'9786043291113', N'Chí Phèo', N'Tuyển tập truyện ngắn tiêu biểu của Nam Cao.', 2021, 1, 2, 3)
INSERT [dbo].[tblSach] ([Id], [ISBN], [Title], [Description], [PublishedYear], [IsActive], [GenreId], [PublisherId]) VALUES (4, N'9786043452225', N'Clean Code', N'Thực hành viết mã nguồn dễ đọc và dễ bảo trì.', 2022, 1, 6, 1)
INSERT [dbo].[tblSach] ([Id], [ISBN], [Title], [Description], [PublishedYear], [IsActive], [GenreId], [PublisherId]) VALUES (5, N'9786043563337', N'1984', N'Tiểu thuyết kinh điển về xã hội phản địa đàng.', 2023, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[tblSach] OFF
GO
INSERT [dbo].[tblSach_TacGia] ([BookId], [AuthorId], [Contribution]) VALUES (1, 1, N'Tác giả')
INSERT [dbo].[tblSach_TacGia] ([BookId], [AuthorId], [Contribution]) VALUES (2, 2, N'Tác giả')
INSERT [dbo].[tblSach_TacGia] ([BookId], [AuthorId], [Contribution]) VALUES (3, 3, N'Tác giả')
INSERT [dbo].[tblSach_TacGia] ([BookId], [AuthorId], [Contribution]) VALUES (4, 5, N'Tác giả')
INSERT [dbo].[tblSach_TacGia] ([BookId], [AuthorId], [Contribution]) VALUES (5, 4, N'Tác giả')
GO
SET IDENTITY_INSERT [dbo].[tblTacGia] ON 

INSERT [dbo].[tblTacGia] ([Id], [FullName], [Biography]) VALUES (1, N'Nguyễn Nhật Ánh', N'Nhà văn Việt Nam, tác giả nhiều tác phẩm dành cho thanh thiếu niên.')
INSERT [dbo].[tblTacGia] ([Id], [FullName], [Biography]) VALUES (2, N'Tô Hoài', N'Nhà văn Việt Nam, tác giả Dế Mèn phiêu lưu ký.')
INSERT [dbo].[tblTacGia] ([Id], [FullName], [Biography]) VALUES (3, N'Nam Cao', N'Nhà văn hiện thực Việt Nam.')
INSERT [dbo].[tblTacGia] ([Id], [FullName], [Biography]) VALUES (4, N'George Orwell', N'Nhà văn và nhà báo người Anh.')
INSERT [dbo].[tblTacGia] ([Id], [FullName], [Biography]) VALUES (5, N'Robert C. Martin', N'Tác giả và chuyên gia về phát triển phần mềm.')
SET IDENTITY_INSERT [dbo].[tblTacGia] OFF
GO
SET IDENTITY_INSERT [dbo].[tblTheLoai] ON 

INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (1, N'Tiểu thuyết', N'Tiểu thuyết dài tập hoặc độc lập', 1, 1)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (2, N'Truyện ngắn', N'Tập truyện ngắn', 1, 1)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (3, N'Thơ', N'Thơ và tuyển tập thơ', 1, 1)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (4, N'Toán học', N'Sách toán học và ứng dụng', 1, 2)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (5, N'Sinh học', N'Sách sinh học và khoa học sự sống', 1, 2)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (6, N'Lập trình', N'Sách ngôn ngữ và kỹ thuật lập trình', 1, 3)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (7, N'Công nghệ thông tin', N'Sách hệ thống và ứng dụng CNTT', 1, 3)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (8, N'Lịch sử Việt Nam', N'Sách lịch sử Việt Nam', 1, 4)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (9, N'Lịch sử thế giới', N'Sách lịch sử thế giới', 1, 4)
INSERT [dbo].[tblTheLoai] ([Id], [Name], [Description], [IsActive], [CategoryId]) VALUES (10, N'Truyện thiếu nhi', N'Truyện phù hợp với thiếu nhi', 1, 5)
SET IDENTITY_INSERT [dbo].[tblTheLoai] OFF
GO
SET IDENTITY_INSERT [dbo].[tblVaiTro] ON 

INSERT [dbo].[tblVaiTro] ([Ma_Vai_tro], [Ten_Vai_tro]) VALUES (1, N'Admin')
INSERT [dbo].[tblVaiTro] ([Ma_Vai_tro], [Ten_Vai_tro]) VALUES (2, N'Librarian')
INSERT [dbo].[tblVaiTro] ([Ma_Vai_tro], [Ten_Vai_tro]) VALUES (3, N'Reader')
SET IDENTITY_INSERT [dbo].[tblVaiTro] OFF
GO
SET IDENTITY_INSERT [dbo].[UserMenu] ON 

INSERT [dbo].[UserMenu] ([UserMenuID], [ItemName], [ItemLevel], [ParentLevel], [ItemOrder], [IsActive], [ItemTarget], [AreaName], [ControllerName], [ActionName], [Icon], [IdName]) VALUES (1, N'Trang chủ', 1, 0, 1, 1, NULL, NULL, N'Home', N'Index', NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserMenu] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AdminUser_Email]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AdminUser_Email] ON [dbo].[AdminUser]
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdminUser_RoleId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_AdminUser_RoleId] ON [dbo].[AdminUser]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AdminUser_UserName]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_AdminUser_UserName] ON [dbo].[AdminUser]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblBanSaoSach_Barcode]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblBanSaoSach_Barcode] ON [dbo].[tblBanSaoSach]
(
	[Barcode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblBanSaoSach_BookId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblBanSaoSach_BookId] ON [dbo].[tblBanSaoSach]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblChiTietMuon_BookCopyId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblChiTietMuon_BookCopyId] ON [dbo].[tblChiTietMuon]
(
	[BookCopyId] ASC
)
WHERE ([ReturnedAtUtc] IS NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblChiTietMuon_LoanRecordId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblChiTietMuon_LoanRecordId] ON [dbo].[tblChiTietMuon]
(
	[LoanRecordId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblDanhMuc_Name]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblDanhMuc_Name] ON [dbo].[tblDanhMuc]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblPhieuMuon_BorrowerId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblPhieuMuon_BorrowerId] ON [dbo].[tblPhieuMuon]
(
	[BorrowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblPhieuMuon_ProcessedById]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblPhieuMuon_ProcessedById] ON [dbo].[tblPhieuMuon]
(
	[ProcessedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblSach_GenreId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblSach_GenreId] ON [dbo].[tblSach]
(
	[GenreId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblSach_ISBN]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblSach_ISBN] ON [dbo].[tblSach]
(
	[ISBN] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblSach_PublisherId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblSach_PublisherId] ON [dbo].[tblSach]
(
	[PublisherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblSach_TacGia_AuthorId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblSach_TacGia_AuthorId] ON [dbo].[tblSach_TacGia]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_tblTaiLieuSo_BookId]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE NONCLUSTERED INDEX [IX_tblTaiLieuSo_BookId] ON [dbo].[tblTaiLieuSo]
(
	[BookId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblTheLoai_CategoryId_Name]    Script Date: 11/4/2026 11:16:37 AM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_tblTheLoai_CategoryId_Name] ON [dbo].[tblTheLoai]
(
	[CategoryId] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AdminUser]  WITH CHECK ADD  CONSTRAINT [FK_AdminUser_tblVaiTro_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[tblVaiTro] ([Ma_Vai_tro])
GO
ALTER TABLE [dbo].[AdminUser] CHECK CONSTRAINT [FK_AdminUser_tblVaiTro_RoleId]
GO
ALTER TABLE [dbo].[tblBanSaoSach]  WITH CHECK ADD  CONSTRAINT [FK_tblBanSaoSach_tblSach_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[tblSach] ([Id])
GO
ALTER TABLE [dbo].[tblBanSaoSach] CHECK CONSTRAINT [FK_tblBanSaoSach_tblSach_BookId]
GO
ALTER TABLE [dbo].[tblChiTietMuon]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietMuon_tblBanSaoSach_BookCopyId] FOREIGN KEY([BookCopyId])
REFERENCES [dbo].[tblBanSaoSach] ([Id])
GO
ALTER TABLE [dbo].[tblChiTietMuon] CHECK CONSTRAINT [FK_tblChiTietMuon_tblBanSaoSach_BookCopyId]
GO
ALTER TABLE [dbo].[tblChiTietMuon]  WITH CHECK ADD  CONSTRAINT [FK_tblChiTietMuon_tblPhieuMuon_LoanRecordId] FOREIGN KEY([LoanRecordId])
REFERENCES [dbo].[tblPhieuMuon] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblChiTietMuon] CHECK CONSTRAINT [FK_tblChiTietMuon_tblPhieuMuon_LoanRecordId]
GO
ALTER TABLE [dbo].[tblPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_tblPhieuMuon_AdminUser_BorrowerId] FOREIGN KEY([BorrowerId])
REFERENCES [dbo].[AdminUser] ([UserID])
GO
ALTER TABLE [dbo].[tblPhieuMuon] CHECK CONSTRAINT [FK_tblPhieuMuon_AdminUser_BorrowerId]
GO
ALTER TABLE [dbo].[tblPhieuMuon]  WITH CHECK ADD  CONSTRAINT [FK_tblPhieuMuon_AdminUser_ProcessedById] FOREIGN KEY([ProcessedById])
REFERENCES [dbo].[AdminUser] ([UserID])
GO
ALTER TABLE [dbo].[tblPhieuMuon] CHECK CONSTRAINT [FK_tblPhieuMuon_AdminUser_ProcessedById]
GO
ALTER TABLE [dbo].[tblSach]  WITH CHECK ADD  CONSTRAINT [FK_tblSach_tblNXB_PublisherId] FOREIGN KEY([PublisherId])
REFERENCES [dbo].[tblNXB] ([Id])
GO
ALTER TABLE [dbo].[tblSach] CHECK CONSTRAINT [FK_tblSach_tblNXB_PublisherId]
GO
ALTER TABLE [dbo].[tblSach]  WITH CHECK ADD  CONSTRAINT [FK_tblSach_tblTheLoai_GenreId] FOREIGN KEY([GenreId])
REFERENCES [dbo].[tblTheLoai] ([Id])
GO
ALTER TABLE [dbo].[tblSach] CHECK CONSTRAINT [FK_tblSach_tblTheLoai_GenreId]
GO
ALTER TABLE [dbo].[tblSach_TacGia]  WITH CHECK ADD  CONSTRAINT [FK_tblSach_TacGia_tblSach_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[tblSach] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblSach_TacGia] CHECK CONSTRAINT [FK_tblSach_TacGia_tblSach_BookId]
GO
ALTER TABLE [dbo].[tblSach_TacGia]  WITH CHECK ADD  CONSTRAINT [FK_tblSach_TacGia_tblTacGia_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[tblTacGia] ([Id])
GO
ALTER TABLE [dbo].[tblSach_TacGia] CHECK CONSTRAINT [FK_tblSach_TacGia_tblTacGia_AuthorId]
GO
ALTER TABLE [dbo].[tblTaiLieuSo]  WITH CHECK ADD  CONSTRAINT [FK_tblTaiLieuSo_tblSach_BookId] FOREIGN KEY([BookId])
REFERENCES [dbo].[tblSach] ([Id])
GO
ALTER TABLE [dbo].[tblTaiLieuSo] CHECK CONSTRAINT [FK_tblTaiLieuSo_tblSach_BookId]
GO
ALTER TABLE [dbo].[tblTheLoai]  WITH CHECK ADD  CONSTRAINT [FK_tblTheLoai_tblDanhMuc_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[tblDanhMuc] ([Id])
GO
ALTER TABLE [dbo].[tblTheLoai] CHECK CONSTRAINT [FK_tblTheLoai_tblDanhMuc_CategoryId]
GO
USE [master]
GO
ALTER DATABASE [QuanLyThuVien] SET  READ_WRITE 
GO
