using AutoMapper;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class TierService
    {
        private readonly TierRepository _repo;
        private readonly IMapper _mapper;

        public TierService(
                TierRepository repo,
                IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<OutboundTier> CreateTier(CreateTierPayload payload)
        {
            var newTier = new Tier();
            _mapper.Map(payload, newTier);

            var savedTier = await _repo.UpsertAsync(newTier);

            var result = new OutboundTier();
            _mapper.Map(savedTier, result);

            return result;
        }
    }
}