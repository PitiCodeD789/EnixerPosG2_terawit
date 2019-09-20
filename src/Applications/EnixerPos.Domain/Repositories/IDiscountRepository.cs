using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels;

namespace EnixerPos.Domain.Repositories
{
    public interface IDiscountRepository
    {
        List<DiscountDto> GetDiscountsByStoreId(int storeId);
        bool AddDiscount(int storeId, DiscountDto discountDto);
        DiscountDto GetDiscount(int storeId, int discountId);
        bool Update(int storeId, DiscountDto discountDto);
    }
}
