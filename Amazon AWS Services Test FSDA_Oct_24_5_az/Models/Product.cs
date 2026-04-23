using System.ComponentModel.DataAnnotations;

namespace Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Models;

public class Product
{
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Description { get; set; }
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }
    [MaxLength(100)]
    public string? Category { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
