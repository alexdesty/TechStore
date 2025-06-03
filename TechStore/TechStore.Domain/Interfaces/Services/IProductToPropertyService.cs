using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Services;

public interface IProductToPropertyService
{
    Task<ProductToProperty> CreateAsync(ProductToProperty productToProperty);
    Task<ProductToProperty> UpdateAsync(ProductToProperty productToProperty);
    Task<bool> DeleteAsync(int id);
    Task<ProductToProperty?> GetAsync(int id);
    Task<IEnumerable<ProductToProperty>> GetAllAsync();
}
