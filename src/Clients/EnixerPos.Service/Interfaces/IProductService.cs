﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Service.Models;

namespace EnixerPos.Service.Interfaces
{
    public interface IProductService
    {
        Task<ResultServiceModel<ResultViewModel>> CreateItem(ItemModel item);
        Task<ResultServiceModel<CategoriesViewModel>> GetAllCategories();
    }
}
