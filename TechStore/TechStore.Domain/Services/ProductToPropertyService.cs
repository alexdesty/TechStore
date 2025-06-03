using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Exceptions;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;

namespace TechStore.Domain.Services;

public class ProductToPropertyService(IUnitOfWork unitOfWork):IProductToPropertyService
{
    public async Task<ProductToProperty> CreateAsync(ProductToProperty productToProperty)
    {
        var addedProductToProperty = await unitOfWork.ProductToPropertyRepository.CreateAsync(productToProperty);
        return await unitOfWork.SaveAsync() > 0 ? addedProductToProperty : throw new DomainException("Property has not been added to product");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var productToProperty = await unitOfWork.ProductToPropertyRepository.GetAsync(id) ?? throw new DomainException("Product with this property not found");
        var deleted = await unitOfWork.ProductToPropertyRepository.DeleteAsync(id);
        await unitOfWork.SaveAsync();
        return deleted;
    }

    public async Task<IEnumerable<ProductToProperty>> GetAllAsync()
    {
        return await unitOfWork.ProductToPropertyRepository.GetAllAsync();
    }

    public async Task<ProductToProperty?> GetAsync(int id)
    {
        return await unitOfWork.ProductToPropertyRepository.GetAsync(id);
    }

    public async Task<ProductToProperty> UpdateAsync(ProductToProperty productToProperty)
    {
        var updatedProductToProperty = unitOfWork.ProductToPropertyRepository.Update(productToProperty);
        return await unitOfWork.SaveAsync() > 0 ? productToProperty
            : throw new DomainException("Product with property has not been updated");
    }
}
