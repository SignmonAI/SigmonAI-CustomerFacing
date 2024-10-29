using core.Repositories;

namespace core.Data.Outbound
{
    public class OutboundUser
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    };

    public class OutboundPaginatedUsers
    {
        public IEnumerable<OutboundUser>? Users { get; set; } = null;
        public PaginationInfo? Pagination { get; set; } = null;
    };
}