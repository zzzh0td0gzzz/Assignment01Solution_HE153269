using BusinessObject.Models;
using DataAccess.Intentions;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly Prn231As1Context _dbcontext;

        public MemberRepository(Prn231As1Context context)
        {
            _dbcontext = context;
        }

        public async Task<Member?> GetMember(string email, string password)
        {
            return await _dbcontext.Members.FirstOrDefaultAsync(mem => mem.Email.ToLower() == email.ToLower() && mem.Password == password);
        }

        public async Task<Member?> GetMember(string email)
        {
            return await _dbcontext.Members.FirstOrDefaultAsync(mem => mem.Email.ToLower() == email.ToLower());
        }

        public async Task<Member?> GetMember(int id)
        {
            return await _dbcontext.Members.FirstOrDefaultAsync(mem => mem.MemberId == id);
        }
    }
}
