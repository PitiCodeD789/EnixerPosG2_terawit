using AutoMapper;
using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Entities;
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
            try
            {
               
                   CategoryEntity categoryEntity = new CategoryEntity();
                categoryEntity.StoreId = storeId;
                categoryEntity.Name = categoryDto.Name;
                categoryEntity.Color = categoryDto.Color;
                categoryEntity.CreateDateTime = DateTime.UtcNow;
                _context.Categories.Add(categoryEntity);
                _context.SaveChanges();
                return true;
            }catch
            {
                return false;
            }
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
            try
            {
                var category = _context.Categories.Where(i => i.StoreId == storeId && i.Id == categoryId).FirstOrDefault();
                return _mapper.Map<CategoryDto>(category);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public CategoryDto GetCategoryByName(string nane)
        {
            var result = _context.Categories.LastOrDefault(x => x.Name == nane);
            //return _mapper.Map<CategoryDto>(result);
            CategoryDto category = new CategoryDto();
            category.StoreId = result.StoreId;
            category.Name = result.Name;
            category.Color = result.Color;           

            category.Id = result.Id;
            return category;
        }

        public int GetCategoryIdByNameAndStoreId(string name,int storeId)
        {
            var result = _context.Categories.Where(x=>x.StoreId == storeId)
                .LastOrDefault(x => x.Name == name);
            return result.Id;
        }

        public bool Update(int storeId, CategoryDto categoryDto)
        {
            try
            {
                var category = _mapper.Map<CategoryEntity>(categoryDto);
                _context.Categories.Update(category);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
