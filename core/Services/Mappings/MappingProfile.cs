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
        }
    }
}