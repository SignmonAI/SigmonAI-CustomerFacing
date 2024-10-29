using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class SubscriptionRepository : SqlServerRepository<Subscription>
    {
        public SubscriptionRepository(SigmonDbContext context) : base(context) {}

        public async Task<Subscription?> FindByUserIdAsync(Guid userId)
            => await _rootSet.FirstOrDefaultAsync(s => s.User!.Id == userId);

    }
}