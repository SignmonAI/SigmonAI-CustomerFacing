using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class UserRepository : SqlServerRepository<User>
    {
        public UserRepository(SigmonDbContext context) : base(context) {}
    }
}