using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class TierRepository : SqlServerRepository<Tier>
    {
        public TierRepository(SigmonDbContext context) : base(context) {}

        public async Task<short> FindMaxTierNumber()
        {
            return await _rootSet.MaxAsync(t => (short?)t.ModelNumber) ?? 0;
        }
    }
}