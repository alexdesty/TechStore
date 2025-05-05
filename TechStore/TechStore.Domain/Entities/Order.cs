using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Enums;

namespace TechStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }  

        public int UserId { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public string DeliveryAddress { get; set; } = string.Empty;

        public bool DeliveryType { get; set; }    

        public int? ShopAddrressId { get; set; }

        public ShopAddress? ShopAddress { get; set; }

        public string DeliveryPhoneNumber { get; set; } = string.Empty;

        public DeliveryStatus DeliveryStatus { get; set; }

    }
}
