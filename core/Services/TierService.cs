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

        async public Task<OutboundTier> CreateTier(TierCreatePayload payload)
        {
            var newTier = new Tier();
            _mapper.Map(payload, newTier);

            var savedTier = await _repo.UpsertAsync(newTier)
                    ?? throw new UpsertFailException("Could not save Tier.");
            
            var result = new OutboundTier();
            _mapper.Map(savedTier, result);

            return result;
        }
    }
}