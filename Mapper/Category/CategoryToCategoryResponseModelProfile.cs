using AutoMapper;
using BusinessObject.API.Category;

namespace Mapper.Category
{
    public class CategoryToCategoryResponseModelProfile : Profile
    {
        public CategoryToCategoryResponseModelProfile()
        {
            CreateMap<BusinessObject.Models.Category, CategoryResponseModel>();
        }
    }
}
