using core.Contexts;
using core.Models;
using Microsoft.EntityFrameworkCore;

namespace core.Repositories
{
    public class LanguageRepository : SqlServerRepository<Language>
    {
        public LanguageRepository(SigmonDbContext context) : base(context) {}
    
         public async Task<Language?> FindByCountryIdAsync(Guid countryId)
            => await _rootSet.FirstOrDefaultAsync(l => l.Country!.Id == countryId);

    }
}