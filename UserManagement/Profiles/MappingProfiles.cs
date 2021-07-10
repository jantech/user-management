using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Core.Entities;
using UserManagement.Core.ViewModels;

namespace UserManagement.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<User, RegisterViewModel>().ReverseMap();
            CreateMap<User, AuthenticateViewModel>().ReverseMap();
            CreateMap<User, UserTokenViewModel>().ReverseMap();

        }
    }
}
