using AutoMapper;
using BusinessObject.API.Category;
using BusinessObject.Models;
using Common;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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

        [Authorize(Roles = CommonConstants.AdminRole)]
        [HttpPost("create")]
        public async Task<ActionResult<CategoryResponseModel>> CreateCategory([MinLength(1)] string name)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to create new category for {User.Identity?.Name}.");
                var category = new Category { CategoryName = name };
                category = await _categoryRepository.CreateCategory(category);
                return _mapper.Map<CategoryResponseModel>(category);
            }
            catch (Exception ex)
            {
                MyLogger.Info($"{_logPrefix} Got exception when creating new category for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
