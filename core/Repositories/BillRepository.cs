using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class BillRepository : SqlServerRepository<Bill>
    {
        public BillRepository(SigmonDbContext context) : base(context) {}

        public async Task<IEnumerable<Bill>> FindBySubscriptionId(Guid subscriptionId)
         => await _rootSet
                    .Where(b => b.Subscription!.Id == subscriptionId)
                    .ToListAsync();
    }
}