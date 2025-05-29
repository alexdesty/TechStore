using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Repositories;

public interface IBaseRepository<T> where T : Entity
{
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(int id); 
    T Update(T entity);

}
