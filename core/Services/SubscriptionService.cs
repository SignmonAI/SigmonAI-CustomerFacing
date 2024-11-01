using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class SubscriptionService(
            SubscriptionRepository repo,
            TierRepository tierRepo,
            IMapper mapper,
            UserRepository userRepo)
    {
        private readonly SubscriptionRepository _repo = repo;
        private readonly UserRepository _userRepo = userRepo;
        private readonly TierRepository _tierRepo = tierRepo;
        private readonly IMapper _mapper = mapper;

        public async Task<Subscription> GetByUserId(Guid userId) => await _repo.FindByUserIdAsync(userId)
                ?? throw new NotFoundException("User not found.");

        public async Task<Subscription> CreateSubscription(SubscriptionCreatePayload payload)
        {
            var newSubscription = new Subscription();
            _mapper.Map(payload, newSubscription);

            var user = await _userRepo.FindByIdAsync(payload.UserId);
            newSubscription.User = user;

            var subscriptionTier = await _tierRepo.FindByIdAsync(payload.TierId)
                    ?? throw new NotFoundException("Subscription tier not found.");
            
            newSubscription.Tier = subscriptionTier;

            var savedSubscription= await _repo.UpsertAsync(newSubscription)
                    ?? throw new UpsertFailException("Subscription could not be inserted.");

            return savedSubscription;
        }

        public async Task DeleteSubscription(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Subscription not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Subscription couldn't be deleted.");

        }
    }
}