using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Services;

public interface ICartService
{
    Task<Cart> CreateAsync(Cart cart);
    Task<Cart> UpdateAsync(Cart cart);
    Task<bool> DeleteAsync(int id);
    Task<Cart?> GetAsync(int id);
    Task<IEnumerable<Cart>> GetAllAsync();
}
