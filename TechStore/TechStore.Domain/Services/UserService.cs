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
namespace TechStore.Domain.Services;

public class UserService(IUnitOfWork unitOfWork):IUserService
{
    public async Task<User> CreateAsync(User User)
    {
        var addedUser = await unitOfWork.UserRepository.CreateAsync(User);
        return await unitOfWork.SaveAsync() > 0 ? addedUser : throw new DomainException("User has not been added");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var User = await unitOfWork.UserRepository.GetAsync(id) ?? throw new DomainException("User not found");
        var deleted = await unitOfWork.UserRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("User has not been deleted");
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await unitOfWork.UserRepository.GetAllAsync();
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
