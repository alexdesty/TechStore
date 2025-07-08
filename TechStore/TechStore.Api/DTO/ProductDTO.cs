using System.ComponentModel.DataAnnotations;

namespace TechStore.Api.DTO;

public class ProductDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; }  = string.Empty;

    public decimal Price { get; set; }

    public string? Image { get; set; }

    public int CategoryId { get; set; }

}
