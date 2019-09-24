using AutoMapper;
using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using Remotion.Linq.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ItemRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AddItem(int storeId, ItemDto itemDto)
        {
            try
            {
                ItemEntity itemEntity = new ItemEntity();
                itemEntity.Name = itemDto.Name;
                itemEntity.StoreId = storeId;
                itemEntity.CategoryId = itemDto.CategoryId;
                itemEntity.Price = itemDto.Price;
                itemEntity.Cost = itemDto.Cost;
                itemEntity.Color = itemDto.Color;
                itemEntity.Option1 = itemDto.Option1;
                itemEntity.Option2 = itemDto.Option2;
                itemEntity.Option3 = itemDto.Option3;
                itemEntity.Option4 = itemDto.Option4;
                itemEntity.Option1Price = itemDto.Option1Price;
                itemEntity.Option2Price = itemDto.Option2Price;
                itemEntity.Option3Price = itemDto.Option3Price;
                itemEntity.Option4Price = itemDto.Option4Price;
                itemEntity.CreateDateTime = DateTime.UtcNow;
                _context.Items.Add(itemEntity);
                _context.SaveChanges();
                return true;             
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public int GetCoutByStoreIdAndCategoryId(int storeId, int id)
        {
            return _context.Items.Where(x => x.CategoryId == id).Where(x => x.StoreId == storeId).Count();
        }

        public ItemDto GetItem(int storeId, int itemId)
        {
            try
            {
                var entity = _context.Items.Find(itemId);
                var item = _mapper.Map<ItemDto>(entity);
                return item;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<ItemDto> GetItemsByStoreId(int storeId)
        {
            try
            {
                var items = _context.Items.Where(i => i.StoreId == storeId).ToList();
                return _mapper.Map<List<ItemDto>>(items);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(int storeId, ItemDto itemDto)
        {
            try
            {
                var item = _mapper.Map<ItemEntity>(itemDto);
                _context.Items.Update(item);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
