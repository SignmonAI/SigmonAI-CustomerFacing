using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;
using Microsoft.AspNetCore.Identity;

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

        public async Task DeleteSubscription(Guid subscriptionId)
        {
            var exists = await _repo.ExistsAsync(subscriptionId);

            if (!exists)
                throw new NotFoundException("Subscription not found.");

            var deleted = await _repo.DeleteByIdAsync(subscriptionId);

            if (!deleted)
                throw new DeleteException("Subscription couldn't be deleted.");
                
        }
    }
}