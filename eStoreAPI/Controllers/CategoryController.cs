using AutoMapper;
using BusinessObject.API.Category;
using Common;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private static readonly string _logPrefix = "[CategoryController]";

        public CategoryController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IMemberRepository memberRepository,
            IConfiguration configuration,
            IMapper mapper)
            : base(productRepository, categoryRepository, orderRepository,
                  orderDetailRepository, memberRepository, configuration, mapper)
        { }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<CategoryResponseModel>>> GetCategories()
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to get all categories for {User.Identity?.Name}.");
                var categories = await _categoryRepository.GetCategories();
                return _mapper.Map<List<CategoryResponseModel>>(categories);
            }
            catch (Exception ex)
            {
                MyLogger.Info($"{_logPrefix} Got exception when getting all categories for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
