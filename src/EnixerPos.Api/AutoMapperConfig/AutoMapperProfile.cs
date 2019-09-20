using AutoMapper;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels.Shifts;
using EnixerPos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnixerPos.Api.AutoMapperConfig
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ShiftdetailDto, GetShiftViewModel>();
            CreateMap<ShiftEntity, ShiftdetailDto>();
        }

    }
}
