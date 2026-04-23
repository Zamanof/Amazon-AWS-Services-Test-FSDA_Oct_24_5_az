using Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Models;
using Microsoft.EntityFrameworkCore;

namespace Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(
            entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Price).HasColumnType("decimal(10, 2)");
            });


        modelBuilder.Entity<Product>().HasData(
    new Product
    {
        Id = 1,
        Name = "iPhone 15 Pro",
        Description = "Latest Apple smartphone with A17 Pro chip",
        Price = 1499.99m,
        Category = "Electronics",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 10, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 2,
        Name = "Samsung Galaxy S24",
        Description = "Flagship Android smartphone by Samsung",
        Price = 1299.99m,
        Category = "Electronics",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 11, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 3,
        Name = "MacBook Pro M3",
        Description = "Apple laptop with M3 chip",
        Price = 2499.50m,
        Category = "Computers",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 12, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 4,
        Name = "Dell XPS 15",
        Description = "Premium Windows ultrabook laptop",
        Price = 1999.00m,
        Category = "Computers",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 13, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 5,
        Name = "Sony WH-1000XM5",
        Description = "Noise cancelling wireless headphones",
        Price = 399.99m,
        Category = "Accessories",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 14, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 6,
        Name = "Apple Watch Series 9",
        Description = "Smart watch with health tracking",
        Price = 599.00m,
        Category = "Wearables",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 7,
        Name = "Logitech MX Master 3S",
        Description = "Advanced wireless productivity mouse",
        Price = 129.99m,
        Category = "Accessories",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 16, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 8,
        Name = "Mechanical Keyboard RGB",
        Description = "Gaming keyboard with RGB lights",
        Price = 159.49m,
        Category = "Accessories",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 9,
        Name = "LG UltraWide Monitor",
        Description = "34 inch curved ultrawide monitor",
        Price = 799.00m,
        Category = "Monitors",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 18, 0, 0, 0, DateTimeKind.Utc)
    },
    new Product
    {
        Id = 10,
        Name = "Gaming Chair Pro",
        Description = "Ergonomic gaming chair with lumbar support",
        Price = 349.99m,
        Category = "Furniture",
        ImageUrl = null,
        CreatedAt = new DateTime(2024, 1, 19, 0, 0, 0, DateTimeKind.Utc)
    }
);

    }
}
