using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Services;

public interface IUserService
{
    Task<User> CreateAsync(User user);
    Task<User> UpdateAsync(User user);
    Task<bool> DeleteAsync(int id);
    Task<User?> GetAsync(int id);
    Task<PaginatedList<User>> GetAllAsync(int pageIndex, int pageSize);
    Task<User> SetRole(User user, UserRole role);
}
