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
            try
            {
                DiscountEntity discountEntity = new DiscountEntity
                { Amount = discountDto.Amount, IsPercentage = discountDto.IsPercentage,
                    StoreId = storeId, DiscountName = discountDto.DiscountName
                };
                _context.Discounts.Add(discountEntity);
                _context.SaveChanges();
                return true;
            }catch
            {
                return false;
            }

          
        }

        public DiscountDto GetDiscount(int storeId, int discountId)
        {
          var result =  _context.Discounts.FirstOrDefault(x => x.StoreId == storeId && x.Id == discountId);
            DiscountDto discountDto = new DiscountDto
            {
                Id = result.Id,
                Amount = result.Amount,
                IsPercentage = result.IsPercentage,
                StoreId = result.StoreId,
                DiscountName = result.DiscountName,
                CreateDateTime = result.CreateDateTime,
                UpdateDateTime = result.UpdateDateTime
            };
            return discountDto;
        }

        public List<DiscountDto> GetDiscountsByStoreId(int storeId)
        {
            try
            {
                var items = _context.Discounts.Where(i => i.StoreId == storeId).OrderByDescending(x=>x.UpdateDateTime).ToList();
                return _mapper.Map<List<DiscountDto>>(items);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool Update(int storeId, DiscountDto discountDto)
        {
            try
            {
                var discount = _mapper.Map<DiscountEntity>(discountDto);
                _context.Discounts.Update(discount);
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
