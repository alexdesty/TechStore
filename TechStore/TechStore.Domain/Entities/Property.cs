using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class Property
{
    [Key]
    public int Id { get; set; } 

    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public List<Category> Categories { get; set; } = [];

    public List<ProductToProperty> ProductsToProperties { get; set; } = [];
}
