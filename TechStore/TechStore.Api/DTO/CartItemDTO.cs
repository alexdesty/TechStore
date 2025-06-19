using TechStore.Domain.Entities;

namespace TechStore.Api.DTO;

public class CartItemDTO
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int CartId { get; set; }
    public int Amount { get; set; }
}
