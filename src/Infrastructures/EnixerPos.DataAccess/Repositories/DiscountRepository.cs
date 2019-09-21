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
    public class DiscountRepository : IDiscountRepository
    {
        private readonly DataContext _context;
        IMapper _mapper;
        public DiscountRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            try
            {
                var items = _context.Discounts.Where(i => i.StoreId == storeId).ToList();
                return _mapper.Map<List<DiscountDto>>(items);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(int storeId, DiscountDto discountDto)
        {
            throw new NotImplementedException();
        }
    }
}
