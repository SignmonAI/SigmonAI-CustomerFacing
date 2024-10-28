using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class SubscriptionRepository : SqlServerRepository<Subscription>
    {
        public SubscriptionRepository(SigmonDbContext context) : base(context) {}
    }
}