using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Services;

public interface ICartItemService
{
    Task<CartItem> CreateAsync(CartItem cartItem);
    Task<CartItem> UpdateAsync(CartItem cartItem);
    Task<bool> DeleteAsync(int id);
    Task<CartItem?> GetAsync(int id);
    Task<IEnumerable<CartItem>> GetAllAsync();
}
