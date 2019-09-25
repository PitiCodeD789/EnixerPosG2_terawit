using AutoMapper;
using EnixerPos.Domain.DtoModels.Auth;
using EnixerPos.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnixerPos.Web
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<List<StaffDto>, List<StaffModel>>();
        }
    }
}
