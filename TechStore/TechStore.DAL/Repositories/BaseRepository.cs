using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechStore.DAL.Data;
using TechStore.Domain.Entities;
using TechStore.Domain.Interfaces.Repositories;

namespace TechStore.DAL.Repositories;

public class BaseRepository<T>(TechStoreDbContext context) : IBaseRepository<T> where T : Entity
{
    public async Task<T> CreateAsync(T entity)
    {

        context.Set<T>().Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity != null)
        {
            context.Set<T>().Remove(entity);
            return await context.SaveChangesAsync() > 0;
        }
        return false;
    }

    public async Task<List<T?>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }
    public async Task<T?> GetAsync(int id)
    {
        return await context.Set<T>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}
