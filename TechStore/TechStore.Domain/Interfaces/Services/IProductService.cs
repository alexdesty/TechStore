using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Services;

public interface IProductService
{
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<Product?> GetAsync(int id);
    Task<PaginatedList<Product>> GetAllAsync(int pageIndex, int pageSize);
}
