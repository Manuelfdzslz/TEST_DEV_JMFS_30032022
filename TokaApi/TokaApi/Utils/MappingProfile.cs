using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokaApi.Data;
using TokaApi.Models;

namespace TokaApi.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PersonasFisica, Tb_PersonasFisica>();
            CreateMap<Tb_PersonasFisica, PersonasFisica>();
            CreateMap<User, Tb_User>();
            CreateMap<Tb_User, User>();
            CreateMap<UserInfo, Tb_UserInfo>();
            CreateMap<Tb_UserInfo, UserInfo>();
        }
    }
}
