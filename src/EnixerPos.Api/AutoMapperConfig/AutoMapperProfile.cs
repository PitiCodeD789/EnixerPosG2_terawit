using AutoMapper;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels;
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
            CreateMap<ItemDto, ItemEntity>();
            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<DiscountDto, DiscountEntity>();
            CreateMap<ItemEntity, ItemDto>();
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<DiscountEntity, DiscountDto>().ReverseMap();
            CreateMap<ItemDto, ItemModel>().ReverseMap();
            CreateMap<List<ItemModel>, List<ItemDto>>().ReverseMap();
        }
    }
}
