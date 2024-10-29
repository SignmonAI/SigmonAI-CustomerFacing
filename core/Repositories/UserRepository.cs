using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class UserRepository : SqlServerRepository<User>
    {
        public UserRepository(SigmonDbContext context) : base(context) {}

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user = await _rootSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
            return user;
        }
    }
}