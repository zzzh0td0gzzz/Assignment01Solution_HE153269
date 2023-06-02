using AutoMapper;
using BusinessObject.API.Order.Request;
using BusinessObject.API.Product.Response;
using BusinessObject.Client.Cart;
using Common;
using eStore.Serivces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace eStore.Pages.Cart
{
    [Authorize(Roles = CommonConstants.MemberRole)]
    public class AddModel : PageModel
    {
        private static readonly string _logPrefix = "[AddModel]";

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AddModel(HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<IActionResult> OnGet()
        {
            try
            {
                var apiClient = new APIClientService(HttpContext, _client);
                var product = await apiClient.Get<ProductResponseModel>($"/api/product/{Id}");
                if (product == null) return RedirectToPage("/Error");

                var detailsRaw = HttpContext.Session.GetString(CommonConstants.CartSession) ?? string.Empty;
                var details = JsonConvert.DeserializeObject<List<ProductCartModel>>(detailsRaw) ?? new();
                var current = details.FirstOrDefault(d => d.ProductId == Id);
                if (current != null) current.Quantity++;
                else details.Add(_mapper.Map<ProductCartModel>(product));

                HttpContext.Session.SetString(CommonConstants.CartSession, JsonConvert.SerializeObject(details));
                return RedirectToPage("/Cart/Index");
            }
            catch (Exception ex)
            {
                MyLogger.Error($"{_logPrefix} Got exception while adding new item to cart. Error: {ex}");
                return RedirectToPage("/Error");
            }
        }
    }
}
