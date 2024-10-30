using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class CountryService(CountryRepository repo, IMapper mapper)
    {
        private readonly CountryRepository _repo = repo;
        private readonly IMapper _mapper = mapper;

        public async Task<Country> GetById(Guid id) => await _repo.FindByIdAsync(id) ?? throw new NotFoundException("Country not found.");

        public async Task<Country> CreateCountry(CountryCreatePayload payload)
        {
            var newCountry = new Country();
            _mapper.Map(payload, newCountry);

            var savedCountry = await _repo.UpsertAsync(newCountry)
                    ?? throw new UpsertFailException("Country could not be inserted.");

            return savedCountry;
        }

        public async Task<Country> UpdateCountry(Guid id, CountryUpdatePayload payload)
        {
            var country = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("Country not found.");

            _mapper.Map(payload, country);

            var savedCountry = await _repo.UpsertAsync(country)
                    ?? throw new UpsertFailException("Country could not be updated.");

            return savedCountry;
        }

        public async Task DeleteCountry(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Country not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Country couldn't be deleted.");
        }
    }
}