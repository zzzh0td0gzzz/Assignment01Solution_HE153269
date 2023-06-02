using AutoMapper;
using BusinessObject.API.Product.Response;
using BusinessObject.Client.Cart;

namespace Mapper.Product
{
    public class ProductResponseModelToProductCartModelProfile : Profile
    {
        public ProductResponseModelToProductCartModelProfile()
        {
            CreateMap<ProductResponseModel, ProductCartModel>()
                .ForMember(des => des.Discount, opt => opt.MapFrom(src => 0))
                .ForMember(des => des.Quantity, opt => opt.MapFrom(src => 1));
        }
    }
}
