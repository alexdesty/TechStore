using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;
using TechStore.Domain.Exceptions;
using TechStore.Domain.Interfaces.Repositories;
using TechStore.Domain.Interfaces.Services;
using TechStore.Domain.Pagination;
namespace TechStore.Domain.Services;

public class UserService(IUnitOfWork unitOfWork):IUserService
{
    public async Task<User> CreateAsync(User user)
    {
        var addedUser = await unitOfWork.UserRepository.CreateAsync(user);
        return await unitOfWork.SaveAsync() > 0 ? addedUser : throw new DomainException("User has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await unitOfWork.UserRepository.GetAsync(id) ?? throw new DomainException("User not found");
        var deleted = await unitOfWork.UserRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("User has not been deleted");
    }

    public async Task<PaginatedList<User>> GetAllAsync(int pageIndex, int pageSize)
    {
        return await unitOfWork.UserRepository.GetAllAsync(pageIndex, pageSize);
    }

    public async Task<User?> GetAsync(int id)
    {
        return await unitOfWork.UserRepository.GetAsync(id);
    }

    public async Task<User> SetRole(User user, UserRole userRole)
    {
        user.Role = userRole;
        return await unitOfWork.SaveAsync() > 0 ? user
     : throw new DomainException("User role has not been set");
    }

    public async Task<User> UpdateAsync(User user)
    {
        var updatedUser = unitOfWork.UserRepository.Update(user);
        return await unitOfWork.SaveAsync() > 0 ? user
            : throw new DomainException("User has not been updated");
    }
}
