using Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Data;
using Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.DTOs;
using Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Models;
using Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Amazon_AWS_Services_Test_FSDA_Oct_24_5_az.Controllers;

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
            CreatedAt = DateTime.UtcNow
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
            CreatedAt = p.CreatedAt
        };
    }
}
