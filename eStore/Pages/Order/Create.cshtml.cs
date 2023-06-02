using AutoMapper;
using BusinessObject.API.Order.Request;
using BusinessObject.API.Order.Response;
using BusinessObject.Client.Cart;
using Common;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStore.Pages.Order
{
    [Authorize(Roles = CommonConstants.MemberRole)]
    public class CreateModel : PageModel
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public CreateModel(HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnPost()
        {
            var detailsRaw = HttpContext.Session.GetString(CommonConstants.CartSession) ?? string.Empty;
            var details = JsonConvert.DeserializeObject<List<ProductCartModel>>(detailsRaw) ?? new();
            if (details == null) return RedirectToPage("/Error");

            var model = new CreateOrderRequestModel
            {
                Freight = CommonConstants.DefaultFreight,
                Details = _mapper.Map<List<CreateOrderDetailRequestModel>>(details)
            };
            var apiClient = new APIClientService(HttpContext, _client);
            var res = await apiClient.Post<OrderInfoResponseModel>("/api/order/create", model);
            if (res == null) return RedirectToPage("/Error");
            return RedirectToPage("/Order/View", new { Id = res.OrderId });
        }
    }
}
