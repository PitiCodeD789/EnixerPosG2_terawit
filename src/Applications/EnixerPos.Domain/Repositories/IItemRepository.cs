using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels;

namespace EnixerPos.Domain.Repositories
{
    public interface IItemRepository
    {
        List<ItemDto> GetItemsByStoreId(int storeId);
        bool AddItem(int storeId, ItemDto itemDto);
        ItemDto GetItem(int storeId, int itemId);
        bool Update(int storeId, ItemDto itemDto);
        int GetCoutByStoreIdAndCategoryId(int storeId, int id);
    }
}
