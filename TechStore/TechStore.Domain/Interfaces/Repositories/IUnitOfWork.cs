using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechStore.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    ICartItemRepository CartItemRepository { get; }
    ICartRepository CartRepository { get; } 
    ICategoryRepository CategoryRepository { get; }
    IOrderRepository OrderRepository { get; }
    IProductRepository ProductRepository { get; }
    IPropertyRepository PropertyRepository { get; }
    IProductToPropertyRepository ProductToPropertyRepository { get; }   
    IShopAddressRepository ShopAddressRepository { get; }
    IUserRepository UserRepository { get; }
    Task<int> SaveAsync();
}
