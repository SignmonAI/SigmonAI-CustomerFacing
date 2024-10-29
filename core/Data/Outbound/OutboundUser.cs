using core.Repositories;

namespace core.Data.Outbound
{
    public readonly record struct OutboundUser(
        Guid Id,
        string Name,
        string Email,
        string Phone
    );

    public readonly record struct OutboundPaginatedUsers(
        IEnumerable<OutboundUser> Users,
        PaginationInfo Pagination
    );
}