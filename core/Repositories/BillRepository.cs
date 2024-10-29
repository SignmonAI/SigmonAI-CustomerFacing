using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class BillRepository : SqlServerRepository<Bill>
    {
        public BillRepository(SigmonDbContext context) : base(context) {}
    }
}