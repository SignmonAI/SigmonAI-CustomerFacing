using core.Contexts;
using core.Models.Seed;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace core.Repositories
{
    public abstract class SqlServerRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly SigmonDbContext _context;
        protected readonly DbSet<TEntity> _rootSet;

        public SqlServerRepository(SigmonDbContext context)
        {
            _context = context;
            _rootSet = _context.Set<TEntity>();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            bool exists = await _rootSet.AnyAsync(e => e.Id.Equals(id));
            if (!exists)
                return false;

            TEntity entityAsIs = await _rootSet.SingleAsync(e => e.Id.Equals(id));

            _rootSet.Remove(entityAsIs);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _rootSet.AnyAsync(e => e.Id.Equals(id));
        }

        public async Task<TEntity?> FindByIdAsync(Guid id)
        {
            return await _rootSet.SingleOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<(IEnumerable<TEntity>, PaginationInfo?)> FindManyAsync(
                PaginationOptions? pagination = null)
        {
            IQueryable<TEntity> query = _rootSet;
            PaginationInfo? paginationInfo = null;

            if (pagination is not null)
            {
                var totalItems = await query.CountAsync();

                if (totalItems <= pagination.Value.Offset )
                    throw new Exception("Offset exceeds maximum of items.");

                query = query.Skip(pagination.Value.Offset).Take(pagination.Value.Take);

                paginationInfo = new PaginationInfo
                {
                    Items = totalItems,
                    CurrentPage = pagination.Value.Offset / pagination.Value.Take + 1,
                    TotalPages = (int) Math.Ceiling((double) totalItems / pagination.Value.Take),
                };
            }

            var data = await query.ToListAsync();

            return (data, paginationInfo);
        }

        public async Task<TEntity?> UpsertAsync(TEntity entity)
        {
            bool entityExists = await _rootSet.AnyAsync(e => e.Id.Equals(entity.Id));

            TEntity foundEntity = entityExists
                    ? await _rootSet.SingleAsync(e => e.Id.Equals(entity.Id))
                    : _rootSet.Add(entity).Entity;
            
            _context.Entry(foundEntity).CurrentValues.SetValues(foundEntity);

            await _context.SaveChangesAsync();
            return foundEntity;
        }
    }
}