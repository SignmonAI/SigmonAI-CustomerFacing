using core.Errors;
using core.Models.Seed;
using core.Repositories;

namespace core.Services.Fixtures
{
    public class BaseFixture<TObject> : IFixture<TObject>
            where TObject : Entity 
    {
        private static TObject? _instance;
        private readonly IRepository<TObject> _repo;
        private readonly bool _generateInDatabase;

        public static TObject? Instance => _instance;
        public bool GenerateInDatabase => _generateInDatabase;

        public BaseFixture(
            IRepository<TObject> repository,
            bool generateInDatabase)
        {
            _instance = default;
            _repo = repository;
            _generateInDatabase = generateInDatabase;
        }

        public async Task PublishInstance(TObject newInstance)
        {
            if (_generateInDatabase)
                newInstance = await CreateIfNotExist(newInstance);

            _instance = newInstance;
        }

        async Task<TObject> CreateIfNotExist(TObject instance)
        {
            bool exists = await _repo.ExistsAsync(instance.Id);
            if (exists)
                return instance;
            
            return await _repo.UpsertAsync(instance)
                ?? throw new UpsertFailException($"State of default {nameof(TObject)} is invalid.");
        }
    }
}