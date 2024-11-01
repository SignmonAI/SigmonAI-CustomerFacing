using core.Models.Seed;

namespace core.Repositories
{
    public readonly record struct PaginationOptions
    {
        public required int Offset { get; init; }
        public required int Take { get; init; }
    }

    public class PaginationInfo
    {
        public int Items { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<bool> ExistsAsync(Guid id);

        Task<TEntity?> FindByIdAsync(Guid id);

        Task<(IEnumerable<TEntity>, PaginationInfo?)> FindManyAsync(PaginationOptions pagination); 

        Task<TEntity?> UpsertAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}