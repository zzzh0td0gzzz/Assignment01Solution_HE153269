using AutoMapper;
using BusinessObject.API.Member.Response;

namespace Mapper.Member
{
    public class MemberToLoginResponseModelProfile : Profile
    {
        public MemberToLoginResponseModelProfile() 
        {
            CreateMap<BusinessObject.Models.Member, LoginResponseModel>()
                .ForMember(des => des.Role, opt => opt.Ignore())
                .ForMember(des => des.Token, opt => opt.Ignore());
        }
    }
}
