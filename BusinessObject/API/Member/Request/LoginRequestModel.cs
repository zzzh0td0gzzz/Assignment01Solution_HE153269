using System.ComponentModel.DataAnnotations;

namespace BusinessObject.API.Member.Request
{
    public class LoginRequestModel
    {
        [MinLength(1)]
        public string Email { get; set; } = null!;
        
        [MinLength(1)]
        public string Password { get; set; } = null!;
    }
}
