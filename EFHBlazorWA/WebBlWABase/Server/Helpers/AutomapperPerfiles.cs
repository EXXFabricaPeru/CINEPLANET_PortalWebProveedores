using AutoMapper;
using EFHBlazzer.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFHBlazzer.Server.Helpers
{
    public class AutomapperPerfiles: Profile
    {
        public AutomapperPerfiles()
        {
            CreateMap<Persona, Persona>()
                .ForMember(x => x.Foto, option => option.Ignore());

        }
    }
}
