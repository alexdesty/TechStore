using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Domain.Entities;

[Keyless]
public class ProductToProperty
{
    public List<Product> Products { get; set; }

    public List<Property> Properties { get; set; }

    public string Value { get; set; } = string.Empty;
}
