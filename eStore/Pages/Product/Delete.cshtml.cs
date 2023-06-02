using Common;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eStore.Pages.Product
{
    [Authorize(Roles = CommonConstants.AdminRole)]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly HttpClient _client;

        public DeleteModel(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> OnGet()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var content = await apiClient.Delete($"/api/product/{Id}");
            if (content == null) return RedirectToPage("/Error");
            return RedirectToPage("/Product/Index");
        }
    }
}
