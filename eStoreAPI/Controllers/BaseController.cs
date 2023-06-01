using AutoMapper;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IProductRepository _productRepository;
        protected readonly ICategoryRepository _categoryRepository;
        protected readonly IOrderRepository _orderRepository;
        protected readonly IOrderDetailRepository _orderDetailRepository;
        protected readonly IMemberRepository _memberRepository;
        protected readonly IConfiguration _configuration;
        protected readonly IMapper _mapper;

        public BaseController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IMemberRepository memberRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _memberRepository = memberRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
    }
}
