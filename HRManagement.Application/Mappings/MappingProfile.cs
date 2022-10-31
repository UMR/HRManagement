using AutoMapper;
using HRManagement.Application.Features.User.Queries;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
