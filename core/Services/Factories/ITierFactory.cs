using core.Models;

namespace core.Services.Factories
{
    public interface ITierFactory
    {
        Task<Tier> GetTier(ClassificationModel model);
    }
}