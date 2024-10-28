using core.Data.Queries;
using core.Repositories;

namespace core.Services
{
    public static class PaginationService
    {
        public static PaginationOptions ToOptions(
            this PaginationQuery query)
        {
            return new PaginationOptions()
            {
                Offset = (query.Page - 1) * query.Count,
                Take = query.Count,
            };
        }
    }
}