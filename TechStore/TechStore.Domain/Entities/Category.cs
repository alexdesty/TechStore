using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public class Category:Entity
{
    public string Name { get; set; } = string.Empty;

    public Product Product { get; set; }

    public List<Property> Properties { get; set; } = [];

}
