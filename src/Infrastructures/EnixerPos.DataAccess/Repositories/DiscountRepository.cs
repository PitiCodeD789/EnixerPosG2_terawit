using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class DiscountRepository : IDiscountRepository
    {
        private readonly DataContext _context;
        public DiscountRepository(DataContext context)
        {
            _context = context;
        }

        public bool AddDiscount(int storeId, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }

        public DiscountDto GetDiscount(int storeId, int discountId)
        {
            throw new NotImplementedException();
        }

        public List<DiscountDto> GetDiscountsByStoreId(int storeId)
        {
            throw new NotImplementedException();
        }

        public bool Update(int storeId, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }
    }
}
