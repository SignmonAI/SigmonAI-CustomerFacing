using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class TierRepository : SqlServerRepository<Tier>
    {
        public TierRepository(SigmonDbContext context) : base(context) {}
    }
}