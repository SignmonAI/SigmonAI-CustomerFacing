using core.Models;
using core.Repositories;

namespace core.Services.Fixtures
{
    public class AdvancedTierFixture(
            IRepository<Tier> repository,
            bool generateInDatabase = false) : BaseFixture<Tier>(repository, generateInDatabase)
    {
    }
}