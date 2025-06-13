using System.ComponentModel.DataAnnotations;

namespace TechStore.Api.DTO;

public class ShopAddressDTO
{
    [Required]
    public string Address { get; set; } = string.Empty;
}
