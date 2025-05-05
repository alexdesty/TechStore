using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Enums;

namespace TechStore.Domain.Entities;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Surname { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Login {  get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty ;

    public UserRole Role {  get; set; } 

    public List<Cart> Carts { get; set; }

    public Order Order { get; set; }
}
