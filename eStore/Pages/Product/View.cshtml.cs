using BusinessObject.API.Product.Response;
using eStore.Serivces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Product
{
    public class ViewModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public ProductResponseModel Product { get; set; } = null!;

        private readonly HttpClient _client;

        public ViewModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var product = await apiClient.Get<ProductResponseModel>($"/api/product/{Id}");
            if (product == null) return RedirectToPage("/Error");
            Product = product;
            return Page();
        }
    }
}
