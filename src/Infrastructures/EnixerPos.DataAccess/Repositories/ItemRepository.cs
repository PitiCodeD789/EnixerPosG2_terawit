using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;
        public ItemRepository(DataContext context)
        {
            _context = context;
        }
        public bool AddItem(int storeId, ItemDto itemDto)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDto> GetCategoriesByStoreId(int storeId)
        {
            throw new NotImplementedException();
        }

        public List<DiscountDto> GetDiscountsByStoreId(int storeId)
        {
            throw new NotImplementedException();
        }

        public ItemDto GetItem(int storeId, int itemId)
        {
            throw new NotImplementedException();
        }

        public List<ItemDto> GetItemsByStoreId(int storeId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int storeId, ItemDto itemDto)
        {
            throw new NotImplementedException();
        }
    }
}
