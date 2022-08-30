using AutoMapper;
using JWT_Minimal_API.Application.Dtos;
using JWT_Minimal_API.Application.Models.Db;

namespace JWT_Minimal_API.Application.Mapping
{
    public class TempAppMappingProfile : Profile
    {
        public TempAppMappingProfile()
        {
            CreateMap<UserRegistrationData,User>();
        }
    }
}
