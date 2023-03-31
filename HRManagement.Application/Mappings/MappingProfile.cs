﻿using AutoMapper;
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
        }
    }
}
