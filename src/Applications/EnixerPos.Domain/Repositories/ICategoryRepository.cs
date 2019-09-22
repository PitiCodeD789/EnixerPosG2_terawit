using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels;

namespace EnixerPos.Domain.Repositories
{
    public interface ICategoryRepository
    {
        List<CategoryDto> GetCategoriesByStoreId(int storeId);
        bool AddCategory(int storeId, CategoryDto categoryDto);
        CategoryDto GetCategory(int storeId, int categoryId);
        CategoryDto GetCategoryByName(string nane);
        bool Update(int storeId, CategoryDto categoryDto);
    }
}
