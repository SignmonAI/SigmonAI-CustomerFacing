namespace core.Data.Queries
{
    public readonly record struct PaginationQuery(
        int Page,
        int Count
    );
}