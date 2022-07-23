using AutoMapper;
using TsentrstroyAPI.Controllers;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Data.AdminPanelJsons.Product;

namespace TsentrstroyAPI.MapProfiles
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<Category, CategoryJsonStruct>();
            CreateMap<SubCategory, SubCategoryJsonStruct>();
            CreateMap<Product, Product>();
        }
    }
}