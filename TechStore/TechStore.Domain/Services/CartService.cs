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

public class CartService(IUnitOfWork unitOfWork) : ICartService
{
    public async Task<Cart> CreateAsync(Cart cart)
    {
        var addedCart = await unitOfWork.CartRepository.CreateAsync(cart);
        return await unitOfWork.SaveAsync() > 0 ? addedCart : throw new DomainException("Cart has not been created");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cart = await unitOfWork.CartRepository.GetAsync(id) ?? throw new DomainException("Cart not found");
        var deleted = await unitOfWork.CartRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("Cart has not been deleted");
    }

    public async Task<IEnumerable<Cart>> GetAllAsync()
    {
        return await unitOfWork.CartRepository.GetAllAsync();
    }

    public async Task<Cart?> GetAsync(int id)
    {
        return await unitOfWork.CartRepository.GetAsync(id);
    }

    public async Task<Cart> UpdateAsync(Cart cart)
    {
        var updatedCart = unitOfWork.CartRepository.Update(cart);
        return await unitOfWork.SaveAsync() > 0 ? cart
            : throw new DomainException("The cart has not beeen updated");
    }
}
