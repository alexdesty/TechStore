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

public class PropertyRepository(TechStoreDbContext context) : BaseRepository<Property>(context), IPropertyRepository
{
    //public async Task<List<Property>> GetPropertyOfCategory(int categoryId)
    //{
    //    return await context.Set<Property>().Where(x => x.Id == categoryId ).ToListAsync();
    //}
}
