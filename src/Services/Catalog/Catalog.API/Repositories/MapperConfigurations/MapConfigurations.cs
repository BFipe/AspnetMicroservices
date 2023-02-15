using AutoMapper;
using Catalog.API.Entities;
using Catalog.API.Repositories.DataTransferObjects.Product;

namespace Catalog.API.Repositories.MapperConfigurations
{
    public class MapConfigurations : Profile
    {
        public MapConfigurations()
        {
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<Product, PostProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
