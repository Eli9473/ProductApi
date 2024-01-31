using AutoMapper;
using Product.App.Contract.Dto;
using Product.Model.Models;

namespace Product.App.Contract.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Products, ProductDto>();
        }
    }
}
