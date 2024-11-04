using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class UserRepository : SqlServerRepository<User>
    {
        public UserRepository(SigmonDbContext context) : base(context) {}

        public async Task<User?> FindByIdEagerAsync(Guid id)
        {
            var user = await _rootSet.Include(u => u.Subscription)
                    .ThenInclude(s => s.Tier)
                    .SingleOrDefaultAsync(u => u.Id.Equals(id));

            return user;
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user = await _rootSet.Include(u => u.Subscription)
                    .ThenInclude(s => s.Tier)
                    .FirstOrDefaultAsync(u => u.Email.Equals(email));

            return user;
        }
    }
}