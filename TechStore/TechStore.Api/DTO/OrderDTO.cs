using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;

namespace TechStore.Api.DTO;

public class OrderDTO
{
    public int Id { get; set; } 
    public int UserId { get; set; }
    public int CartId { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public bool DeliveryType { get; set; }
    public int? ShopAddressId { get; set; }
    public string DeliveryPhoneNumber { get; set; }
    public DeliveryStatus DeliveryStatus { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
}
