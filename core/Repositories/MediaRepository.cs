using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class MediaRepository : SqlServerRepository<Media>
    {
        public MediaRepository(SigmonDbContext context) : base(context) {}
    }
}