using AutoMapper;
using BusinessObject;
using BusinessObject.API.Member.Response;
using BusinessObject.API.Order.Request;
using BusinessObject.API.Order.Response;
using Common;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private static readonly string _logPrefix = "[OrderController]";

        public OrderController(IProductRepository productRepository,
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
        public async Task<ActionResult<PagingModel<OrderResponseModel>>> GetOrders(OrderRequestModel model)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to get orders for {User.Identity?.Name}.");
                var role = User.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                int? userId = null;
                if (role == CommonConstants.MemberRole) userId = int.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                var orders = await _orderRepository.GetOrders(userId, model.StartDate, model.EndDate, model.PageIndex, model.PageSize);

                var ordersRs = new List<OrderResponseModel>();
                foreach (var item in orders.Items)
                {
                    var orderRs = _mapper.Map<OrderResponseModel>(item);
                    orderRs.Member = _mapper.Map<MemberResponseModel>(item.Member);
                    ordersRs.Add(orderRs);
                }
                return new PagingModel<OrderResponseModel>
                {
                    Items = ordersRs,
                    TotalPage = orders.TotalPage
                };
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Start to get orders for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderInfoResponseModel>> GetOrder([FromRoute] int id)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to get orders for {User.Identity?.Name}.");
                var role = User.Claims.First(claim => claim.Type == ClaimTypes.Role).Value;
                int? userId = null;
                if (role == CommonConstants.MemberRole) userId = int.Parse(User.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                var order = await _orderRepository.GetOrder(id, userId);
                if (order == null) return NotFound();

                var orderDetails = await _orderDetailRepository.GetOrderDetails(id);

                var res = _mapper.Map<OrderInfoResponseModel>(order);
                res.Member = _mapper.Map<MemberResponseModel>(order.Member);
                res.Details = _mapper.Map<List<OrderDetailResponseModel>>(orderDetails);
                return res;
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Start to get orders for {User.Identity?.Name}. Error: {ex}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
