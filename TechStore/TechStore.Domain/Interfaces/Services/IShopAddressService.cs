using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Interfaces.Services;

public interface IShopAddressService
{
    Task<ShopAddress> CreateAsync(ShopAddress shopAddress);
    Task<ShopAddress> UpdateAsync(ShopAddress shopAddress);
    Task<bool> DeleteAsync(int id);
    Task<ShopAddress?> GetAsync(int id);
    Task<PaginatedList<ShopAddress>> GetAllAsync(int pageIndex, int pageSize);
}
