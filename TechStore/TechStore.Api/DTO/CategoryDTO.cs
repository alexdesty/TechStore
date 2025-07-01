using System.ComponentModel.DataAnnotations;

namespace TechStore.Api.DTO;

public class CategoryDTO
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

}
