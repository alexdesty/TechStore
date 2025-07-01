using TechStore.Domain.Entities;

namespace TechStore.Api.DTO;

public class CartDTO
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public bool IsPurchased { get; set; }
}
