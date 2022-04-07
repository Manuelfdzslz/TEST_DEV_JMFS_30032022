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
        }
    }
}
