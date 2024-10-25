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
        }
    }
}