using AutoMapper;
using Entities.DataTransferObject;
using Entities.Models;

namespace WebApi.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>();
        }
    }
}
