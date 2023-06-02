using BusinessObject.API.Category;
using BusinessObject.API.Product.Request;
using BusinessObject.API.Product.Response;
using Common;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Product
{
    [Authorize(Roles = CommonConstants.AdminRole)]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public ProductUpdateRequestModel Product { get; set; } = null!;

        public List<CategoryResponseModel> Categories { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public bool Error { get; set; } = false;

        [BindProperty(SupportsGet = true)]
        public string? Message { get; set; }

        private readonly HttpClient _client;

        public EditModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var product = await apiClient.Get<ProductResponseModel>($"/api/product/{Id}");
            if (product == null) return RedirectToPage("/Error");

            var categories = await apiClient.Get<List<CategoryResponseModel>>("/api/category");
            if (categories == null) return RedirectToPage("/Error");

            Product = new ProductUpdateRequestModel
            {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                UnitPrice = product.UnitPrice,
                UnitsInStock = product.UnitsInStock,
                Weight = product.Weight
            };
            Categories = categories;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var content = await apiClient.Put($"/api/product/{Id}", Product);
            if (content == null) return RedirectToPage("/Product/Edit", new { Id, Message = "Update Failed", Error = true });
            return RedirectToPage("/Product/Edit", new { Id, Message = "Update Successfully", Error = false });
        }
    }
}
