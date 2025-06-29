using AutoMapper;
using TechStore.Api.DTO;
using TechStore.Domain.Entities;
namespace TechStore.Api;


public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Category, CategoryDTO>();
        CreateMap<CategoryDTO, Category>();
        CreateMap<ShopAddress, ShopAddressDTO>();
        CreateMap<ShopAddressDTO, ShopAddress>();
        CreateMap<Product, ProductDTO>();
        CreateMap<ProductDTO, Product>();
        CreateMap<User, UserDTO>();
        CreateMap<UserDTO, User>();
        CreateMap<Cart, CartDTO>();
        CreateMap<CartDTO, Cart>();
        CreateMap<CartItem, CartItemDTO>();
        CreateMap<CartItemDTO, CartItem>();
        CreateMap<CartItemToCreateDTO, CartItem>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDTO, Order>();
    }
}
