using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Exceptions;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Services;

public class ShopAddressService(IUnitOfWork unitOfWork):IShopAddressService
{
    public async Task<ShopAddress> CreateAsync(ShopAddress shopAddress)
    {
        var addedShopAddress = await unitOfWork.ShopAddressRepository.CreateAsync(shopAddress);
        return await unitOfWork.SaveAsync() > 0 ? addedShopAddress : throw new DomainException("Shop address has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var shopAddress = await unitOfWork.ShopAddressRepository.GetAsync(id) ?? throw new DomainException("Shop address not found");
        var deleted = await unitOfWork.ShopAddressRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("Shop address has not been deleted");
    }

    public async Task<PaginatedList<ShopAddress>> GetAllAsync(int pageIndex, int pageSize)
    {
        return await unitOfWork.ShopAddressRepository.GetAllAsync(pageIndex, pageSize);
    }

    public async Task<ShopAddress?> GetAsync(int id)
    {
        return await unitOfWork.ShopAddressRepository.GetAsync(id);
    }

    public async Task<ShopAddress> UpdateAsync(ShopAddress shopAddress)
    {
        var updatedShopAddress = unitOfWork.ShopAddressRepository.Update(shopAddress);
        return await unitOfWork.SaveAsync() > 0 ? shopAddress
            : throw new DomainException("Shop address has not been updated");
    }
}
