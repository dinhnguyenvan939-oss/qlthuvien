using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace qlthuvien.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Tác phẩm văn học trong nước và quốc tế", true, "Văn học" },
                    { 2, "Sách khoa học tự nhiên và xã hội", true, "Khoa học" },
                    { 3, "Sách công nghệ, kỹ thuật và tin học", true, "Công nghệ" },
                    { 4, "Sách lịch sử Việt Nam và thế giới", true, "Lịch sử" },
                    { 5, "Sách và truyện dành cho thiếu nhi", true, "Thiếu nhi" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CategoryId", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Tiểu thuyết dài tập hoặc độc lập", true, "Tiểu thuyết" },
                    { 2, 1, "Tập truyện ngắn", true, "Truyện ngắn" },
                    { 3, 1, "Thơ và tuyển tập thơ", true, "Thơ" },
                    { 4, 2, "Sách toán học và ứng dụng", true, "Toán học" },
                    { 5, 2, "Sách sinh học và khoa học sự sống", true, "Sinh học" },
                    { 6, 3, "Sách ngôn ngữ và kỹ thuật lập trình", true, "Lập trình" },
                    { 7, 3, "Sách hệ thống và ứng dụng CNTT", true, "Công nghệ thông tin" },
                    { 8, 4, "Sách lịch sử Việt Nam", true, "Lịch sử Việt Nam" },
                    { 9, 4, "Sách lịch sử thế giới", true, "Lịch sử thế giới" },
                    { 10, 5, "Truyện phù hợp với thiếu nhi", true, "Truyện thiếu nhi" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
