﻿using AutoMapper;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : BaseController
    {
        public OrderDetailController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IMemberRepository memberRepository,
            IConfiguration configuration,
            IMapper mapper)
            : base(productRepository, categoryRepository, orderRepository,
                  orderDetailRepository, memberRepository, configuration, mapper)
        { }
    }
}
