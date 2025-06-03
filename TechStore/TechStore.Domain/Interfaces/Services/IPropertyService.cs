using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;

namespace TechStore.Domain.Interfaces.Services;

public interface IPropertyService
{
    Task<Property> CreateAsync(Property property);
    Task<Property> UpdateAsync(Property property);
    Task<bool> DeleteAsync(int id);
    Task<Property?> GetAsync(int id);
    Task<IEnumerable<Property>> GetAllAsync();
}
