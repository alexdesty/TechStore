using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using TechStore.Domain.Entities;
using TechStore.Domain.Enums;

namespace TechStore.DAL.Data;

public class TechStoreDbContext(DbContextOptions<TechStoreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<ShopAddress> ShopAddresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<User>().HasIndex(l => l.Login).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<Product>().HasIndex(p => p.CategoryId).IsUnique(false);
        modelBuilder.Entity<Order>().HasIndex(p => p.ShopAddressId).IsUnique(false);
        modelBuilder.Entity<ShopAddress>().HasIndex(s => s.Address).IsUnique();


        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(10, 2);

        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 1,
            Name = "Admin",
            Surname = "Admin",
            Email = "emailadmin@mail.com",
            Login = "admin",
            Password = "pass9393",
            Role = UserRole.Admin
        });
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 2,
            Name = "Manager",
            Surname = "Manager",
            Email = "emailmanager@mail.com",
            Login = "manager",
            Password = "pass8787",
            Role = UserRole.Manager
        });
        modelBuilder.Entity<User>().HasData(new User()
        {
            Id = 3,
            Name = "User",
            Surname = "Manager",
            Email = "emailuser@mail.com",
            Login = "user",
            Password = "pass7373",
            Role = UserRole.User
        });

        modelBuilder.Entity<ShopAddress>().HasData(new ShopAddress()
        {
            Id = 1,
            Address = "Minsk, Independence avenue, 1"
        });
        modelBuilder.Entity<ShopAddress>().HasData(new ShopAddress()
        {
            Id = 2,
            Address = "Minsk, International street, 1"
        });

        modelBuilder.Entity<Category>().HasData(new Category()
        {
            Id = 1,
            Name = "Phones"
        });
        modelBuilder.Entity<Category>().HasData(new Category()
        {
            Id = 2,
            Name = "Laptops"
        });

        modelBuilder.Entity<Product>().HasData(new Product()
        {
            Id = 1,
            Name = "Iphone 14 Pro Max",
            Price = 999,
            Description = "Nice phone",
            CategoryId = 1,
        });
        modelBuilder.Entity<Product>().HasData(new Product()
        {
            Id = 2,
            Name = "Apple MacBook Pro 16.2 M3 Pro",
            Price = 3000,
            Description = "Nice laptop",
            CategoryId = 2,
        });
    }
}
