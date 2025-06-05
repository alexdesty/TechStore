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

public class PropertyService(IUnitOfWork unitOfWork):IPropertyService
{
    public async Task<Property> CreateAsync(Property property)
    {
        var addedProperty = await unitOfWork.PropertyRepository.CreateAsync(property);
        return await unitOfWork.SaveAsync() > 0 ? addedProperty : throw new DomainException("Property has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var property = await unitOfWork.PropertyRepository.GetAsync(id) ?? throw new DomainException("Property not found");
        var deleted = await unitOfWork.PropertyRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("Property has not been deleted");
    }

    public async Task<IEnumerable<Property>> GetAllAsync()
    {
        return await unitOfWork.PropertyRepository.GetAllAsync();
    }

    public async Task<Property?> GetAsync(int id)
    {
        return await unitOfWork.PropertyRepository.GetAsync(id);
    }

    public async Task<Property> UpdateAsync(Property property)
    {
        var updatedProperty = unitOfWork.PropertyRepository.Update(property);
        return await unitOfWork.SaveAsync() > 0 ? property
            : throw new DomainException("Property has not been updated");
    }
}
