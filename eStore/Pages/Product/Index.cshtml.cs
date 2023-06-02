using BusinessObject;
using BusinessObject.API.Product.Request;
using BusinessObject.API.Product.Response;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Product
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public PagingModel<ProductResponseModel> Products { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public string? Name { get; set; } = null;
        
        [BindProperty(SupportsGet = true)]
        public bool? UnitPriceSortAsc { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 10;

        private readonly HttpClient _client;

        public IndexModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var data = new ProductRequestModel
            {
                Name = Name,
                PageIndex = PageIndex,
                PageSize = PageSize,
                UnitPriceSortAsc = UnitPriceSortAsc
            };
            var products = await apiClient.Post<PagingModel<ProductResponseModel>>("/api/product/all", data);
            if (products == null) return RedirectToPage("/Error");
            Products = products;
            return Page();
        }
    }
}
