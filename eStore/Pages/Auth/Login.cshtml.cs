using BusinessObject.API.Member.Request;
using BusinessObject.API.Member.Response;
using eStore.Serivces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace eStore.Pages.Auth
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; } = null!;

        [BindProperty]
        public string Password { get; set; } = null!;

        [BindProperty(SupportsGet = true)]
        public string? Error { get; set; } = null;

        private readonly HttpClient _client;

        public LoginModel(HttpClient client)
        {
            _client = client;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var apiClient = new APIClientService(HttpContext, _client);
            var model = new LoginRequestModel
            {
                Email = Email,
                Password = Password
            };
            var res = await apiClient.Post<LoginResponseModel>("/api/member/login", model);
            if (res == null) return RedirectToPage("/Auth/Login", new { Error = "Login Failed!" });
            HttpContext.Session.SetString("accessToken", res.Token);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, res.MemberId.ToString()),
                new Claim(ClaimTypes.Name, res.Email),
                new Claim(ClaimTypes.Email, res.Email),
                new Claim(ClaimTypes.Role, res.Role)
            };
            var claimsIdentity = new ClaimsIdentity(claims, "login");
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync("CookieAuthentication", claimsPrincipal);
            return RedirectToPage("/Index");
        }
    }
}
