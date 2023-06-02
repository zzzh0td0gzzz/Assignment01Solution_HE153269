using BusinessObject.Client.Cart;
using Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStore.Pages.Cart
{
    [Authorize(Roles = CommonConstants.MemberRole)]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public int Id { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        [BindProperty]
        public string? Update { get; set; }

        [BindProperty]
        public string? Delete { get; set; }

        public List<ProductCartModel> Details { get; set; } = new();

        public IActionResult OnGet()
        {
            var detailsRaw = HttpContext.Session.GetString(CommonConstants.CartSession) ?? string.Empty;
            Details = JsonConvert.DeserializeObject<List<ProductCartModel>>(detailsRaw) ?? new();
            return Page();
        }

        public IActionResult OnPost()
        {
            var detailsRaw = HttpContext.Session.GetString(CommonConstants.CartSession) ?? string.Empty;
            Details = JsonConvert.DeserializeObject<List<ProductCartModel>>(detailsRaw) ?? new();
            var detail = Details.FirstOrDefault(d => d.ProductId == Id);
            if (detail != null)
            {
                if (Delete != null) Details.Remove(detail);
                else if (Update != null)
                {
                    if (Quantity <= 0) Details.Remove(detail);
                    else detail.Quantity = Quantity;
                }
            }

            HttpContext.Session.SetString(CommonConstants.CartSession, JsonConvert.SerializeObject(Details));
            return RedirectToPage("/Cart/Index");
        }
    }
}
