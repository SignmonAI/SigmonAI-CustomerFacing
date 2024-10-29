using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class SubscriptionService
    {
        private readonly SubscriptionRepository _repo;
        private readonly IMapper _mapper;

        public SubscriptionService(SubscriptionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Subscription> CreateSubscription(SubscriptionCreatePayload payload)
        {
            var newSubscription = new Subscription();
            _mapper.Map(payload, newSubscription);

            var savedSubscription= await _repo.UpsertAsync(newSubscription) ?? throw new UpsertFailException("Subscription could not be inserted.");

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