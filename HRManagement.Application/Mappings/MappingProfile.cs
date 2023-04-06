using AutoMapper;
using HRManagement.Application.Dtos.Permissions;
using HRManagement.Application.Dtos.Roles;
using HRManagement.Application.Dtos.Users;
using HRManagement.Domain.Entities;

namespace HRManagement.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserForListDto>().ReverseMap();
            CreateMap<User, UserForCreateDto>().ReverseMap();
            CreateMap<User, UserForUpdaterDto>().ReverseMap();
            CreateMap<Role, RoleForListDto>().ReverseMap();
            CreateMap<Role, RoleForCreateDto>().ReverseMap();
            CreateMap<Role, RoleForUpdateDto>().ReverseMap();
            CreateMap<Permission, PermissionForListDto>().ReverseMap();
            CreateMap<Permission, PermissionForCreateDto>().ReverseMap();
            CreateMap<Permission, PermissionForUpdateDto>().ReverseMap();
        }
    }
}
