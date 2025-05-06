using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.Domain.Enums;

namespace TechStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        [DeleteBehavior(DeleteBehavior.NoAction)]
        public User User { get; set; }  = new User();

        public int UserId { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; } = new Cart();

        public string DeliveryAddress { get; set; } = string.Empty;

        public bool DeliveryType { get; set; }    

        public int? ShopAddrressId { get; set; }

        public ShopAddress? ShopAddress { get; set; }

        public string DeliveryPhoneNumber { get; set; } = string.Empty;

        public DeliveryStatus DeliveryStatus { get; set; }

    }
}
