using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Enums;

namespace TechStore.Domain.Entities;

public class Order:Entity
{

    [DeleteBehavior(DeleteBehavior.NoAction)]
    public User User { get; set; }  

    public int UserId { get; set; }

    public int CartId { get; set; }

    public Cart Cart { get; set; } 

    public string DeliveryAddress { get; set; } = string.Empty;

    public bool DeliveryType { get; set; }    

    public int? ShopAddressId { get; set; }

    public ShopAddress? ShopAddress { get; set; }

    public string DeliveryPhoneNumber { get; set; } 

    public DeliveryStatus DeliveryStatus { get; set; }

    public DateTime OrderDate {  get; set; } = DateTime.Now;

}
