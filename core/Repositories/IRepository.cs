using core.Models.Seed;

namespace core.Repositories
{
    public class PaginationOptions
    {
        public int Offset { get; set; }
        public int Take { get; set; }
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

        Task<(IEnumerable<TEntity>, PaginationInfo?)> FindManyAsync(PaginationOptions? pagination = null); 

        Task<TEntity?> UpsertAsync(TEntity entity);

        Task<bool> DeleteByIdAsync(Guid id);
    }
}