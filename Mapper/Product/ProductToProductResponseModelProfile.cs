using AutoMapper;
using BusinessObject.API.Product.Response;
using BusinessObject.Models;

namespace Mapper.Product
{
    public class ProductToProductResponseModelProfile : Profile
    {
        public ProductToProductResponseModelProfile()
        {
            CreateMap<BusinessObject.Models.Product, ProductResponseModel>()
                .ForMember(des => des.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
        }
    }
}
