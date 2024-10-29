using AutoMapper;
using core.Data.Payloads;
using core.Models;

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
        }
    }
}