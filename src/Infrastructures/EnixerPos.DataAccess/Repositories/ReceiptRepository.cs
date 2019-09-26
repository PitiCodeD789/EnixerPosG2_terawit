using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly DataContext _context;
        public ReceiptRepository(DataContext context)
        {
            _context = context;
        }

        public void Create(ReceiptEntity receiptEntity)
        {
            _context.Add(receiptEntity);
            _context.SaveChanges();
        }

        public List<ReceiptEntity> GetReceiptByShiftId(int shiftId, string storeEmail)
        {
            List<ReceiptEntity> receipts = _context.Receipt.Where(x => x.ShiftId == shiftId).Where(x=>x.StoreEmail == storeEmail.ToLower()).OrderByDescending(x=>x.Id).ToList();
            return receipts;
        }

        public List<ReceiptEntity> GetReceiptsByDateAndShift(DateTime date ,int shiftId)
        {
            List<ReceiptEntity> receipts = _context.Receipt.Where(x => x.CreateDateTime.Date == date)
                .Where(x=>x.ShiftId == shiftId).OrderByDescending(x=>x.Id).ToList();
            return receipts;
        }

        public void Update(ReceiptEntity receiptEntity)
        {
            _context.SaveChanges();
        }
    }
}
