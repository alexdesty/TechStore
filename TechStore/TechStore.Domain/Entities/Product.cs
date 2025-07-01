using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class Product:Entity
{
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public byte[]? Photo {  get; set; } 

    public string Description { get; set; } = string.Empty;

    public int CategoryId { get; set; }

    public Category Category { get; set; }

}
