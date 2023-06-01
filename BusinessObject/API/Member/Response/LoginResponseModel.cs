namespace BusinessObject.API.Member.Response
{
    public class LoginResponseModel
    {
        public int MemberId { get; set; }

        public string Email { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string Role { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
