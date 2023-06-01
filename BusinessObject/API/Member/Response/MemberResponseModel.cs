namespace BusinessObject.API.Member.Response
{
    public class MemberResponseModel
    {
        public int MemberId { get; set; }

        public string Email { get; set; } = null!;

        public string? CompanyName { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }
    }
}
