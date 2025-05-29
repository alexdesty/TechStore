using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Entities;

public abstract class Entity
{
    [Key]
    public int Id { get; set; } 
}
