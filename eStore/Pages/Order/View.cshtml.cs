using BusinessObject.API.Order.Response;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Order
{
    [Authorize]
    public class ViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public OrderInfoResponseModel OrderInfo { get; set; } = null!;
        private readonly HttpClient _client;

        public ViewModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var res = await apiClient.Get<OrderInfoResponseModel>($"/api/order/{Id}");
            if (res == null) return RedirectToPage("/Error");
            else OrderInfo = res;
            return Page();
        }
    }
}
