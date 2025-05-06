using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class Cart
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public bool IsPurchased { get; set; }

    public User User { get; set; }

    public Order Order { get; set; }

    public List<CartItem> Items { get; set; } = [];

}
