using BusinessObject.Models;

namespace DataAccess.Intentions
{
    public interface IMemberRepository
    {
        public Task<Member?> GetMember(string email, string password);

        public Task<Member?> GetMember(string email);

        public Task<Member?> GetMember(int id);
    }
}
