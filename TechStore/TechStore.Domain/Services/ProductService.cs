using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TechStore.Domain.Entities;
using TechStore.Domain.Exceptions;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Pagination;

namespace TechStore.Domain.Services;

public class ProductService(IUnitOfWork unitOfWork, IFileUploadService fileUploadService):IProductService
{
    private readonly string ImagesFolderPath = "static/images";
    private readonly HashSet<string> AllowedFileExtensions = new HashSet<string>
    {
        ".jpg", ".jpeg", ".png", ".gif"
    };
    public async Task<Product> CreateAsync(Product product)
    {
        var addedProduct = await unitOfWork.ProductRepository.CreateAsync(product);
        return await unitOfWork.SaveAsync() > 0 ? addedProduct : throw new DomainException("The Product has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await unitOfWork.ProductRepository.GetAsync(id) ?? throw new DomainException("Product not found");
        var deleted = await unitOfWork.ProductRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("Product has not been deleted");
    }

    public async Task<PaginatedList<Product>> GetAllAsync(int pageIndex, int pageSize)
    {
        return await unitOfWork.ProductRepository.GetAllAsync(pageIndex, pageSize);
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

    public async Task<string> UploadImageAsync(int productId, IFormFile file)
    {
        var product = await GetAsync(productId);
        if (product == null)
        {
            throw new DomainException($"Product with id {productId} not found.");
        }

        var fileName = file.FileName;
        var fileExtension = Path.GetExtension(fileName).ToLowerInvariant();
        if (!AllowedFileExtensions.Contains(fileExtension))
        {
            throw new DomainException($"File extension {fileExtension} is not allowed." + $" Allowed extensions are: {string.Join(", ", AllowedFileExtensions)}");
        }

        var uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";

        await fileUploadService.UploadFileAsync(ImagesFolderPath, uniqueFileName, file);

        product.Image = uniqueFileName;
        await unitOfWork.SaveAsync();

        return uniqueFileName;
    }
}
