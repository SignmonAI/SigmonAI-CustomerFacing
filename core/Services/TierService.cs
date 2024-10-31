using AutoMapper;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class TierService(
            TierRepository repo,
            IMapper mapper)
    {
        private readonly TierRepository _repo = repo;
        private readonly IMapper _mapper = mapper;

        public async Task<OutboundTier> CreateTier(TierCreatePayload payload)
        {
            var newTier = new Tier();
            _mapper.Map(payload, newTier);

            var currentMaxNumber = await _repo.FindMaxTierNumber();
            newTier.ModelNumber = (short)(currentMaxNumber + 1);

            var savedTier = await _repo.UpsertAsync(newTier)
                    ?? throw new UpsertFailException("New subscription tier could not be inserted.");

            return EntityToResult(savedTier);
        }

        public async Task<OutboundTier> UpdateTier(
                Guid id,
                TierUpdatePayload payload)
        {
            var tier = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("Subscription tier not found.");

            _mapper.Map(payload, tier);

            var savedTier = await _repo.UpsertAsync(tier)
                    ?? throw new UpsertFailException("Subscription tier could not be inserted.");
                
            return EntityToResult(savedTier);
        }

        private OutboundTier EntityToResult(Tier entity) => _mapper.Map<OutboundTier>(entity);
    }
}