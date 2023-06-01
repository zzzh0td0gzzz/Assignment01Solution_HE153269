using AutoMapper;
using BusinessObject.API.Order.Response;

namespace Mapper.Order
{
    public class OrderToOrderResponseModelProfile : Profile
    {
        public OrderToOrderResponseModelProfile()
        {
            CreateMap<BusinessObject.Models.Order, OrderResponseModel>()
                .ForMember(des => des.Member, opt => opt.Ignore());
        }
    }
}
