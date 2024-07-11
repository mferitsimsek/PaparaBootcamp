using AutoMapper;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;


namespace PaparaBootcamp.Application.Mappings
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDTO, ProductEntity>().ReverseMap();
        }
    }
}
