using AutoMapper;
using BusinessObject.API.Order.Response;

namespace Mapper.Order
{
    public class OrderToOrderInfoResponseModelProfile : Profile
    {
        public OrderToOrderInfoResponseModelProfile() 
        {
            CreateMap<BusinessObject.Models.Order, OrderInfoResponseModel>()
                .ForMember(des => des.Details, opt => opt.Ignore())
                .ForMember(des => des.Member, opt => opt.Ignore());
        }
    }
}
