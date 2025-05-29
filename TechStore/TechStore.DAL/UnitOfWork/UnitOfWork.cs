using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.DAL.Data;
using TechStore.DAL.Repositories;
using TechStore.Domain.Interfaces.Repositories;

namespace TechStore.DAL.UnitOfWork;

public class UnitOfWork : IDisposable, IUnitOfWork
{
    private readonly TechStoreDbContext _context;
    private ICartItemRepository? cartItemRepository;
    private ICartRepository? cartRepository;
    private ICategoryRepository? categoryRepository;
    private IOrderRepository? orderRepository;
    private IProductRepository? productRepository;
    private IPropertyRepository? propertyRepository;
    private IProductToPropertyRepository productToPropertyRepository;
    private IShopAddressRepository? shopAddressRepository;
    private IUserRepository? userRepository;

    public UnitOfWork(TechStoreDbContext context)
    {
        _context = context;
    }

    public ICartItemRepository CartItemRepository
    {
        get
        {
            if (this.cartItemRepository == null)
            {
                this.cartItemRepository = new CartItemRepository(_context);
            }
            return cartItemRepository;
        }
    }

    public ICartRepository CartRepository
    {
        get
        {
            if (this.cartRepository == null)
            {
                this.cartRepository = new CartRepository(_context);
            }
            return cartRepository;
        }
    }

    public ICategoryRepository CategoryRepository
    {
        get
        {
            if (this.categoryRepository == null)
            {
                this.categoryRepository = new CategoryRepository(_context);
            }
            return categoryRepository;
        }
    }

    public IOrderRepository OrderRepository
    {
        get
        {
            if (this.orderRepository == null)
            {
                this.orderRepository = new OrderRepository(_context);
            }
            return orderRepository;
        }
    }

    public IProductRepository ProductRepository
    {
        get
        {
            if (this.productRepository == null)
            {
                this.productRepository = new ProductRepository(_context);
            }
            return productRepository;
        }
    }

    public IPropertyRepository PropertyRepository
    {
        get
        {
            if (this.propertyRepository == null)
            {
                this.propertyRepository = new PropertyRepository(_context);
            }
            return propertyRepository;
        }
    }

    public IProductToPropertyRepository ProductToPropertyRepository
    {
        get
        {
            if (this.productToPropertyRepository == null)
            {
                this.productToPropertyRepository = new ProductToPropertyRepository(_context);
            }
            return productToPropertyRepository;
        }
    }

    public IShopAddressRepository ShopAddressRepository

    {
        get
        {
            if (this.shopAddressRepository == null)
            {
                this.shopAddressRepository = new ShopAddressRepository(_context);
            }
            return shopAddressRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            if (this.userRepository == null)
            {
                this.userRepository = new UserRepository(_context);
            }
            return userRepository;
        }
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    public virtual void Dispose(bool disposing)
    {
        if (!this.disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        this.disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
