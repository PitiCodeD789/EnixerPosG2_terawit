using AutoMapper;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Api.ViewModels.Shifts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.DtoModels.Sale;
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
            CreateMap<OpenShiftViewModel,ShiftdetailDto>();
            CreateMap<OpenShiftViewModel,ShiftdetailDto>().ReverseMap();
            CreateMap<ManageCashCommand, ManageCashDto>();
            CreateMap<ManageCashCommand, ManageCashDto>().ReverseMap();
            CreateMap<ItemDto, ItemEntity>();
            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryModel, CategoryDto>();
            CreateMap<CategoryModel, CategoryDto>().ReverseMap();
            CreateMap<DiscountDto, DiscountEntity>();
            CreateMap<DiscountEntity, DiscountDto>().ReverseMap();
            CreateMap<DiscountModel, DiscountDto>();
            CreateMap<DiscountModel, DiscountDto>().ReverseMap();
            CreateMap<ItemEntity, ItemDto>();
            CreateMap<ItemDto, ItemModel>().ReverseMap();
            CreateMap<List<ItemModel>, List<ItemDto>>().ReverseMap();
            CreateMap<List<ReceiptDto>, List<ReceiptViewModel>>();
            CreateMap<ReceiptDto, ReceiptViewModel>();
        }
    }
}
