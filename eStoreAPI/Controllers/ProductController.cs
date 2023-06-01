using AutoMapper;
using BusinessObject;
using BusinessObject.API.Product.Request;
using BusinessObject.API.Product.Response;
using BusinessObject.Models;
using Common;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private static readonly string _logPrefix = "[ProductController]";

        public ProductController(IProductRepository productRepository,
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
        [HttpPost("all")]
        public async Task<ActionResult<PagingModel<ProductResponseModel>>> GetProducts(ProductRequestModel model)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to get products for {User.Identity?.Name}.");
                var products = await _productRepository.GetProducts(model.Name, model.UnitPriceSortAsc, model.PageIndex, model.PageSize);
                return new PagingModel<ProductResponseModel>
                {
                    Items = _mapper.Map<List<ProductResponseModel>>(products.Items),
                    TotalPage = products.TotalPage
                };
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when getting products for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseModel>> GetProduct([FromRoute] int id)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to get product {id} for {User.Identity?.Name}.");
                var product = await _productRepository.GetProduct(id);
                return _mapper.Map<ProductResponseModel>(product);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when getting product {id} for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500);
            }
        }

        [Authorize(Roles = CommonConstants.AdminRole)]
        [HttpPost("create")]
        public async Task<ActionResult<ProductResponseModel>> CreateProduct(ProductCreateRequestModel model)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to create new product for {User.Identity?.Name}.");

                var product = _mapper.Map<Product>(model);
                product = await _productRepository.CreateProduct(product);
                var res = _mapper.Map<ProductResponseModel>(product);
                return StatusCode(201, res);
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when creating new product for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500);
            }
        }

        [Authorize(Roles = CommonConstants.AdminRole)]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct([FromRoute] int id, [FromBody] ProductRequestModel model)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to update product {id} for {User.Identity?.Name}.");
                var product = await _productRepository.GetProduct(id);
                if (product == null) return NotFound();

                product = _mapper.Map<Product>(model);
                product.ProductId = id;
                await _productRepository.UpdateProduct(product);
                return NoContent();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when updating product {id} for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500);
            }
        }

        [Authorize(Roles = CommonConstants.AdminRole)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct([FromRoute] int id)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to delete product {id} for {User.Identity?.Name}.");
                var product = await _productRepository.GetProduct(id);
                if (product == null) return NotFound();

                await _productRepository.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception when deleting product {id} for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500);
            }
        }
    }
}
