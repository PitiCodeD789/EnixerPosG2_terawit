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
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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
