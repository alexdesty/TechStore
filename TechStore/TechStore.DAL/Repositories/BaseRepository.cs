using System;
using System.Collections.Generic;
using System.Fabric.Query;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Data;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Pagination;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TechStore.DAL.Repositories;

public class BaseRepository<T>(TechStoreDbContext context) : IBaseRepository<T> where T : Entity
{
    public async Task<T> CreateAsync(T entity)
    {

        await context.Set<T>().AddAsync(entity);
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
            return true;
        }
        return false;
    }

    public async Task<PaginatedList<T>> GetAllAsync(int pageIndex, int pageSize)
    {
        var items = await context.Set<T>()
            .OrderBy(b=>b.Id)
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
       var count = await context.Set<T>().CountAsync();
        var totalPages = (int)Math.Ceiling(count/(double)pageSize);
        return new PaginatedList<T>(items, pageIndex, totalPages);
    }

    public async Task<T?> GetAsync(int id)
    {
        return await context.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public T Update(T entity)
    {
        context.Set<T>().Update(entity);
        return entity;
    }
}
