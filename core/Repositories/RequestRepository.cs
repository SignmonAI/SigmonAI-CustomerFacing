using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class RequestRepository : SqlServerRepository<Request>
    {
        public RequestRepository(SigmonDbContext context) : base(context) {}
    }
}