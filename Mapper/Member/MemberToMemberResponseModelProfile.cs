using AutoMapper;
using BusinessObject.API.Member.Response;

namespace Mapper.Member
{
    public class MemberToMemberResponseModelProfile : Profile
    {
        public MemberToMemberResponseModelProfile()
        {
            CreateMap<BusinessObject.Models.Member, MemberResponseModel>();
        }
    }
}
