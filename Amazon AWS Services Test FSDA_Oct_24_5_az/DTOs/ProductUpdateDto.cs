using System.ComponentModel.DataAnnotations;

namespace Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.DTOs;

public class ProductUpdateDto
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
}
