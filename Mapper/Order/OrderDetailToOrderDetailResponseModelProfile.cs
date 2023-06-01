using AutoMapper;
using BusinessObject.API.Order.Response;
using BusinessObject.Models;

namespace Mapper.Order
{
    public class OrderDetailToOrderDetailResponseModelProfile : Profile
    {
        public OrderDetailToOrderDetailResponseModelProfile()
        {
            CreateMap<OrderDetail, OrderDetailResponseModel>()
                .ForMember(des => des.ProductName, opt => opt.MapFrom(src => src.Product.ProductName));
        }
    }
}
