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
    }

}
