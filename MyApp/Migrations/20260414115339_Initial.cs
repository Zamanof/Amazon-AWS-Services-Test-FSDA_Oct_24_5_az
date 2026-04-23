using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "CreatedAt", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Electronics", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Latest Apple smartphone with A17 Pro chip", null, "iPhone 15 Pro", 1499.99m },
                    { 2, "Electronics", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Utc), "Flagship Android smartphone by Samsung", null, "Samsung Galaxy S24", 1299.99m },
                    { 3, "Computers", new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Apple laptop with M3 chip", null, "MacBook Pro M3", 2499.50m },
                    { 4, "Computers", new DateTime(2024, 1, 13, 0, 0, 0, 0, DateTimeKind.Utc), "Premium Windows ultrabook laptop", null, "Dell XPS 15", 1999.00m },
                    { 5, "Accessories", new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Utc), "Noise cancelling wireless headphones", null, "Sony WH-1000XM5", 399.99m },
                    { 6, "Wearables", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Smart watch with health tracking", null, "Apple Watch Series 9", 599.00m },
                    { 7, "Accessories", new DateTime(2024, 1, 16, 0, 0, 0, 0, DateTimeKind.Utc), "Advanced wireless productivity mouse", null, "Logitech MX Master 3S", 129.99m },
                    { 8, "Accessories", new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Gaming keyboard with RGB lights", null, "Mechanical Keyboard RGB", 159.49m },
                    { 9, "Monitors", new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Utc), "34 inch curved ultrawide monitor", null, "LG UltraWide Monitor", 799.00m },
                    { 10, "Furniture", new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Utc), "Ergonomic gaming chair with lumbar support", null, "Gaming Chair Pro", 349.99m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
