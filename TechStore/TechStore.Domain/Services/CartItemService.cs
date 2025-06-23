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

public class CartItemService(IUnitOfWork unitOfWork) : ICartItemService
{
    public async Task<CartItem> CreateAsync(CartItem cartItem)
    {
        var addedCartItem = await unitOfWork.CartItemRepository.CreateAsync(cartItem);
        return await unitOfWork.SaveAsync() > 0 ? addedCartItem : throw new DomainException("Product has not been added to the cart");
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cartItem = await unitOfWork.CartItemRepository.GetAsync(id) ?? throw new DomainException("Product in cart not found");
        var deleted = await unitOfWork.CartItemRepository.DeleteAsync(id);
        return await unitOfWork.SaveAsync() > 0 ? deleted : throw new DomainException("Product has not been deleted from the cart");
    }

    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await unitOfWork.CartItemRepository.GetAllAsync();
    }

    public async Task<CartItem?> GetAsync(int id)
    {
        return await unitOfWork.CartItemRepository.GetAsync(id);
    }

    public async Task<List<CartItem>> GetItemsByCartIdAsync(int id)
    {
        return await unitOfWork.CartItemRepository.GetItemsByCartIdAsync(id);
    }

    public async Task<CartItem> UpdateAsync(CartItem cartItem)
    {
        var updatedCartItem = unitOfWork.CartItemRepository.Update(cartItem);
        return await unitOfWork.SaveAsync() > 0 ? cartItem
            : throw new DomainException("Item in cart not updated");
    }

}
