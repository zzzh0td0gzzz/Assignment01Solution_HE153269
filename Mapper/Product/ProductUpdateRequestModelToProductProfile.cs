using AutoMapper;
using BusinessObject.API.Product.Request;
using BusinessObject.Models;

namespace Mapper.Product
{
    public class ProductUpdateRequestModelToProductProfile : Profile
    {
        public ProductUpdateRequestModelToProductProfile()
        {
            CreateMap<ProductUpdateRequestModel, BusinessObject.Models.Product>()
                .ForMember(des => des.ProductId, opt => opt.Ignore())
                .ForMember(des => des.Category, opt => opt.Ignore())
                .ForMember(des => des.OrderDetails, opt => opt.Ignore());
        }
    }
}
