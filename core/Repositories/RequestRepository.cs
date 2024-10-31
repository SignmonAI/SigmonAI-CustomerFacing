using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class RequestRepository : SqlServerRepository<Request>
    {
        public RequestRepository(SigmonDbContext context) : base(context) {}

        // public async Task<Request> LazySelectAsync(Guid id)
        // {
        //     _rootSet.Include(r => r.Media).
        // }
    }
}