using AutoMapper;
using PaparaBootcamp.Domain.DTOs;
using PaparaBootcamp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaparaBootcamp.Application.Mappings
{
    public class ProductMappingProfile:Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
