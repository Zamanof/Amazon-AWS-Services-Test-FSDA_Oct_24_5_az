using System.ComponentModel.DataAnnotations;

namespace MyApp.DTOs;

public class ProductCreateDto
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Description { get; set; }
    [MaxLength(100)]
    public string? Category { get; set; }
    [Range(0.01, 999999.99)]
    public decimal Price { get; set; }
    public IFormFile? Image { get; set; }
    public DateTime? DiscountStart { get; set; }
    public DateTime? DiscountEnd { get; set; }
}
