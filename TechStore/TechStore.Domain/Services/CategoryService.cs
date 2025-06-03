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

public class CategoryService(IUnitOfWork unitOfWork):ICategoryService
{
    public async Task<Category> CreateAsync(Category category)
    {
        var addedCategory = await unitOfWork.CategoryRepository.CreateAsync(category);
        return await unitOfWork.SaveAsync() > 0 ? addedCategory : throw new DomainException("Category has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await unitOfWork.CategoryRepository.GetAsync(id) ?? throw new DomainException("Category not found");
        var deleted = await unitOfWork.CategoryRepository.DeleteAsync(id);
        await unitOfWork.SaveAsync();
        return deleted;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await unitOfWork.CategoryRepository.GetAllAsync();
    }

    public async Task<Category?> GetAsync(int id)
    {
        return await unitOfWork.CategoryRepository.GetAsync(id);
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        var updatedCategory = unitOfWork.CategoryRepository.Update(category);
        return await unitOfWork.SaveAsync() > 0 ? category
            : throw new DomainException("Category has not been updated");
    }
}
