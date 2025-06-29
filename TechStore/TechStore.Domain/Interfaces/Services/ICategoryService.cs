using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Services;

public interface ICategoryService
{
    Task<Category> CreateAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task<bool> DeleteAsync(int id);
    Task<Category?> GetAsync(int id);
    Task<PaginatedList<Category>> GetAllAsync(int pageIndex, int pageSize);
}
