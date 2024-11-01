using AutoMapper;
using core.Data.Payloads;
using core.Errors;
using core.Models;
using core.Repositories;

namespace core.Services
{
    public class LanguageService(LanguageRepository repo, IMapper mapper, CountryRepository countryRepo)
    {
        private readonly LanguageRepository _repo = repo;
        private readonly CountryRepository _countryRepo = countryRepo;

        private readonly IMapper _mapper = mapper;

        public async Task<Language> GetById(Guid id) => await _repo.FindByIdEagerAsync(id) ?? throw new NotFoundException("Language not found.");

        public async Task<Language> GetByCountryId(Guid countryId) => await _repo.FindByCountryIdAsync(countryId) ?? throw new NotFoundException("Country not found.");

        public async Task<Language> CreateLanguage(LanguageCreatePayload payload)
        {
            var newLanguage = new Language();

            _mapper.Map(payload, newLanguage);

            var country = await _countryRepo.FindByIdAsync(payload.CountryId);
            newLanguage.Country = country;

            var savedLanguage = await _repo.UpsertAsync(newLanguage) ?? throw new UpsertFailException("Language could not be inserted.");

            return savedLanguage;
        }

        public async Task<Language> UpdateLanguage(Guid id, LanguageUpdatePayload payload)
        {
            var language = await _repo.FindByIdAsync(id)
                    ?? throw new NotFoundException("Language not found.");

            _mapper.Map(payload, language);

            if (payload.CountryId.HasValue)
            {
                var country = await _countryRepo.FindByIdAsync(payload.CountryId.Value);
                language.Country = country;
            }

            var savedLanguage = await _repo.UpsertAsync(language)
                    ?? throw new UpsertFailException("Language could not be updated.");

            return savedLanguage;
        }

        public async Task DeleteLanguage(Guid id)
        {
            var exists = await _repo.ExistsAsync(id);

            if (!exists)
                throw new NotFoundException("Language not found.");

            var deleted = await _repo.DeleteByIdAsync(id);

            if (!deleted)
                throw new DeleteException("Language couldn't be deleted.");
        }
    }
}