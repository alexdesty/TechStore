using System.ComponentModel.DataAnnotations;

namespace TechStore.Api.DTO;

public class ShopAddressDTO
{
    public int Id { get; set; }

    [Required]
    public string Address { get; set; } = string.Empty;
}
