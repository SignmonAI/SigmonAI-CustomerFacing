using core.Contexts;
using core.Models;

namespace core.Repositories
{
    public class CountryRepository : SqlServerRepository<Country>
    {
        public CountryRepository(SigmonDbContext context) : base(context) {}
    }
}