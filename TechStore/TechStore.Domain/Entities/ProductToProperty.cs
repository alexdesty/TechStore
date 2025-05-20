using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TechStore.Domain.Entities;

public class ProductToProperty
{
    public int Id { get; set; } 

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public Property Property { get; set; }

    public int PropertyId { get; set; }

    public string Value { get; set; } = string.Empty;


}
