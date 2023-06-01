using AutoMapper;
using BusinessObject.API.Member.Request;
using BusinessObject.API.Member.Response;
using BusinessObject.Models;
using Common;
using DataAccess.Intentions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : BaseController
    {
        private static readonly string _logPrefix = "[MemberController]";

        public MemberController(IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IMemberRepository memberRepository,
            IConfiguration configuration,
            IMapper mapper)
            : base(productRepository, categoryRepository, orderRepository,
                  orderDetailRepository, memberRepository, configuration, mapper)
        { }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                MyLogger.Info($"{_logPrefix} Start to process login request {model.Email}.");
                Member? member = null;
                var isAdmin = false;
                if (model.Email.ToLower() == _configuration["AdminEmail"]?.ToLower() && model.Password == _configuration["Password"])
                {
                    member = new Member
                    {
                        MemberId = 0,
                        Email = model.Email
                    };
                    isAdmin = true;
                }
                else member = await _memberRepository.GetMember(model.Email, model.Password);

                if (member == null) return StatusCode(404);
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtKey"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _configuration["JwtIssuer"],
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, member.MemberId.ToString()),
                        new Claim(ClaimTypes.Email, member.Email),
                        new Claim(ClaimTypes.Role, isAdmin ? CommonConstants.AdminRole : CommonConstants.MemberRole)
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var response = _mapper.Map<LoginResponseModel>(member);
                response.Role = isAdmin ? CommonConstants.AdminRole : CommonConstants.MemberRole;
                response.Token = tokenHandler.WriteToken(token);
                return response;
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception while processing login request {model.Email}. Error: {ex}");
                return StatusCode(500);
            }
        }
    }
}
