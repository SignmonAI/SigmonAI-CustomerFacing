using core.Repositories;

namespace core.Data.Outbound
{
    public class OutboundCountry
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? PhoneCode { get; set; }
    };

    public class OutboundPaginatedCountries
    {
        public IEnumerable<OutboundCountry>? Countries { get; set; } = null;
        public PaginationInfo? Pagination { get; set; } = null;
    };
}