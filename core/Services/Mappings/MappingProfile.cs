using AutoMapper;
using core.Data.Outbound;
using core.Data.Payloads;
using core.Models;
using core.Repositories;

namespace core.Services.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreatePayload, User>();

            CreateMap<UserUpdatePayload, User>()
                    .ForAllMembers(opts => opts.Condition(
                        (src, dest, srcMember) => srcMember is not null
                    ));

            CreateMap<SubscriptionCreatePayload, Subscription>();

            CreateMap<BillCreatePayload, Bill>();

            CreateMap<BillUpdatePayload, Bill>()
                    .ForAllMembers(opts => opts.Condition(
                        (src, dest, srcMember) => srcMember is not null
                    ));

            CreateMap<CountryCreatePayload, Country>();

            CreateMap<CountryUpdatePayload, Country>()
                    .ForAllMembers(opts => opts.Condition(
                        (src, dest, srcMember) => srcMember is not null
                    ));

            CreateMap<TierCreatePayload, Tier>();

            CreateMap<TierUpdatePayload, Tier>()
                    .ForAllMembers(opts => opts.Condition(
                        (src, dest, srcMember) => srcMember is not null));
        
            CreateMap<LanguageCreatePayload, Language>();

            CreateMap<LanguageUpdatePayload, Language>()
                    .ForAllMembers(opts => opts.Condition(
                        (src, dest, srcMember) => srcMember is not null
                    ));

            OutboundMapping();
        }

        private void OutboundMapping()
        {
            CreateMap<User, OutboundUser>();

            CreateMap<(IEnumerable<User>, PaginationInfo?), OutboundPaginatedUsers>()
                    .ForMember(
                        dest => dest.Users,
                        opt => opt.MapFrom(src => src.Item1))
                    .ForMember(
                        dest => dest.Pagination,
                        opt => opt.MapFrom(src => src.Item2));

            CreateMap<Country, OutboundCountry>();

            CreateMap<(IEnumerable<Country>, PaginationInfo?), OutboundPaginatedCountries>()
                    .ForMember(
                        dest => dest.Countries,
                        opt => opt.MapFrom(src => src.Item1))
                    .ForMember(
                        dest => dest.Pagination,
                        opt => opt.MapFrom(src => src.Item2));
        }
    }
}