using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class RequestRepository : SqlServerRepository<Request>
    {
        public RequestRepository(SigmonDbContext context) : base(context) {}

        public async Task<Request> EagerFindByIdAsync(Guid id)
        {
            var request = await _rootSet
                .Include(r => r.Media)
                .SingleOrDefaultAsync(e => e.Id.Equals(id));

            return request!;
        }

        public async Task<List<Request>> EagerFindManyAsync()
        {
            var request = await _rootSet
                .Include(r => r.Media)
                .Include(r => r.User)
                .ToListAsync();

            return request!;
        }
    }
}