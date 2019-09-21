using AutoMapper;
using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool AddCategory(int storeId, CategoryDto categoryDto)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetCategoriesByStoreId(int storeId)
        {
            try
            {
                var items = _context.Categories.Where(i => i.StoreId == storeId).ToList();
                return _mapper.Map<List<CategoryDto>>(items);
            }
            catch (Exception e)
            {
                return null;
            }
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
