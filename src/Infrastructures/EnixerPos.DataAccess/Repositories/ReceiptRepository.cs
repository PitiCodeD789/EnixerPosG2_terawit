using EnixerPos.DataAccess.Contexts;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EnixerPos.DataAccess.Repositories
{
    class ReceiptRepository : IReceiptRepository
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

        public List<ReceiptEntity> GetReceiptByShiftId(int shiftId, string storeEmail, string posIMEI)
        {
            List<ReceiptEntity> receipts = _context.Receipt.Where(x => x.ShiftId == shiftId).Where(x=>x.StoreEmail == storeEmail.ToLower()).Where(x=>x.PosImei == posIMEI).ToList();
            return receipts;
        }

        public List<ReceiptEntity> GetReceiptsByDate(DateTime date)
        {
            List<ReceiptEntity> receipts = _context.Receipt.Where(x => x.CreateDateTime.Date == date).ToList();
            return receipts;
        }

        public void Update(ReceiptEntity receiptEntity)
        {
            _context.SaveChanges();
        }
    }
}
