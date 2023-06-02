using AutoMapper;
using BusinessObject.API.Order.Request;
using BusinessObject.Client.Cart;

namespace Mapper.Product
{
    public class ProductCartModelProfile : Profile
    {
        public ProductCartModelProfile() 
        {
            CreateMap<ProductCartModel, CreateOrderDetailRequestModel>();
        }
    }
}
