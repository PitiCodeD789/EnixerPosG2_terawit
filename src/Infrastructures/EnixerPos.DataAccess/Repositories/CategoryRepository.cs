using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddCategory(int storeId, CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetCategoriesByStoreId(int storeId)
        {
            throw new NotImplementedException();
        }

        public CategoryDto GetCategory(int storeId, int categoryId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int storeId, CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }
    }
}
