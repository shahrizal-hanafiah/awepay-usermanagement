using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<AppUser, UserDto>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                src.DateOfBirth.CalculateAge())
                );
            CreateMap<UserUpdateDto, AppUser>();
        }
    }
}
