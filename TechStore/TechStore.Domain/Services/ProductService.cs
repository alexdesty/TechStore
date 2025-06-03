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

public class ProductService(IUnitOfWork unitOfWork):IProductService
{
    public async Task<Product> CreateAsync(Product product)
    {
        var addedProduct = await unitOfWork.ProductRepository.CreateAsync(product);
        return await unitOfWork.SaveAsync() > 0 ? addedProduct : throw new DomainException("The Product has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(id) ?? throw new DomainException("Product not found");
        var deleted = await unitOfWork.ProductRepository.DeleteAsync(id);
        await unitOfWork.SaveAsync();
        return deleted;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await unitOfWork.ProductRepository.GetAllAsync();
    }

    public async Task<Product?> GetAsync(int id)
    {
        return await unitOfWork.ProductRepository.GetAsync(id);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var updatedProduct = unitOfWork.ProductRepository.Update(product);
        return await unitOfWork.SaveAsync() > 0 ? product
            : throw new DomainException("Product has not been updated");
    }
}
