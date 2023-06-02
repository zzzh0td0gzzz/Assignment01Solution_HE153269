using AutoMapper;
using BusinessObject;
using BusinessObject.API.Order.Request;
using BusinessObject.API.Order.Response;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Order
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        [BindProperty(SupportsGet = true)]
        public DateTime? StartDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? EndDate { get; set; }

        public PagingModel<OrderResponseModel> Orders { get; set; } = new();

        private readonly HttpClient _client;

        public IndexModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var model = new OrderRequestModel
            {
                EndDate = EndDate,
                StartDate = StartDate,
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            var res = await apiClient.Post<PagingModel<OrderResponseModel>>("/api/order/all", model);
            if (res == null) return RedirectToPage("/Error");
            Orders = res;
            return Page();
        }
    }
}
