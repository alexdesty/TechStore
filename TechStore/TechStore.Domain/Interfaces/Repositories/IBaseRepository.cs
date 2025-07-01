using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(int id);
   Task<PaginatedList<T>> GetAllAsync(int pageIndex, int pageSize);
    Task<T> GetAsync(int id); 
    T Update(T entity);
}
