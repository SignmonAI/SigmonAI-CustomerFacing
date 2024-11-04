using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services.Factories
{
    public class DefaultTierFactory(TierRepository repo) : ITierFactory
    {
        private readonly TierRepository _repo = repo;

        private readonly IDictionary<string, Tier> _tiers = new Dictionary<string, Tier>()
        {
            {
                "Free",
                new Tier(
                    modelDescription: "Free",
                    modelNumber: ClassificationModel.FREE)
            },
            {
                "Intermediate",
                new Tier(
                    modelDescription: "Intermediate",
                    modelNumber: ClassificationModel.INTERMEDIATE)
            },
            {
                "Advanced",
                new Tier(
                    modelDescription: "Advanced",
                    modelNumber: ClassificationModel.ADVANCED)
            },
        };

        public async Task<Tier> GetTier(ClassificationModel model)
        {
            var tier = await _repo.FetchByNumber(model);

            Tier result;

            if (tier is null)
            {
                result = model switch
                {
                    ClassificationModel.INTERMEDIATE => _tiers["Intermediate"],
                    ClassificationModel.ADVANCED => _tiers["Advanced"],
                    _ => _tiers["Free"]
                };

                await _repo.UpsertAsync(result);

                return result;
            }

            result = tier;
            return result;
        }
    }
}