using System.ComponentModel.DataAnnotations;

namespace TechStore.Api.DTO;

public class CategoryDTO
{
    [Required]
    public string Name { get; set; } = string.Empty;
}
