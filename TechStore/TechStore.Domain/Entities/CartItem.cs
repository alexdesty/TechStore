using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class CartItem
{
    [Key]
    public int Id { get; set; }    

    public List<Product> Products { get; set; }

    public List<Cart> Carts { get; set; }

    public int Amount { get; set; }
}
