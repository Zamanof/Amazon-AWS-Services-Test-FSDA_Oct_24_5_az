using MyApp.Data;
using MyApp.DTOs;
using MyApp.Models;
using MyApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IStorageService _storageService;

    public ProductsController(AppDbContext context, IStorageService storageService)
    {
        _context = context;
        _storageService = storageService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductReadDto>>> GetProducts()
    {
        var products = await _context.Products
                                     .OrderByDescending(p => p.CreatedAt)
                                     .Select(p => MapToReadDto(p))
                                     .ToListAsync();
        return Ok(products);
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<ProductReadDto>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(MapToReadDto(product));
    }

    [HttpPost]
    public async Task<ActionResult<ProductReadDto>> CreateProduct([FromForm] ProductCreateDto productDto)
    {
        if (!ModelState.IsValid)        
            return BadRequest(ModelState);
        ValidateDiscountRange(productDto.DiscountStart, productDto.DiscountEnd);

        string? imageUrl = null;

        if (productDto.Image != null)
        {
            imageUrl = await _storageService.UploadFileAsync(productDto.Image);
        }

        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Category = productDto.Category,
            Price = productDto.Price,
            ImageUrl = imageUrl,
            CreatedAt = DateTime.UtcNow,
            DiscountStart = productDto.DiscountStart,
            DiscountEnd = productDto.DiscountEnd,
        };
        
        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, MapToReadDto(product));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ProductReadDto>> UpdateProduct(int id, [FromForm] ProductCreateDto productDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        ValidateDiscountRange(productDto.DiscountStart, productDto.DiscountEnd);

        var product = await _context.Products.FindAsync(id);
        if (product == null)        
            return NotFound();
        

        if (productDto.Image != null)
        {
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                await _storageService.DeleteFileAsync(product.ImageUrl);
            }
            product.ImageUrl = await _storageService.UploadFileAsync(productDto.Image);
        }
        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Category = productDto.Category;
        product.Price = productDto.Price;
        product.DiscountStart = productDto.DiscountStart;
        product.DiscountEnd = productDto.DiscountEnd;

        await _context.SaveChangesAsync();
        return Ok(MapToReadDto(product));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
            return NotFound();

        if (!string.IsNullOrEmpty(product.ImageUrl))
        {
            await _storageService.DeleteFileAsync(product.ImageUrl);
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

    private static ProductReadDto MapToReadDto(Product p)
    {
        return new ProductReadDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Category = p.Category,
            Price = p.Price,
            ImageUrl = p.ImageUrl,
            CreatedAt = p.CreatedAt,
            DiscountStart = p.DiscountStart,
            DiscountEnd = p.DiscountEnd,
            IsDiscountActive = p.IsDiscountActive
        };
    }

    private void ValidateDiscountRange(DateTime? discountStart, DateTime? discountEnd)
    {
        if (discountStart.HasValue && discountEnd.HasValue && discountStart > discountEnd)
        {
            ModelState.AddModelError("DiscountEnd", "Discount end date must be after the start date.");
        }
        if (discountStart.HasValue != discountEnd.HasValue)
        {
            ModelState.AddModelError("DiscountStart", "Both discount start and end dates must be provided together.");
        }
    }
}
