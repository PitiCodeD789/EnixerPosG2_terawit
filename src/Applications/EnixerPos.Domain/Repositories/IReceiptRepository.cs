using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.Entities;

namespace EnixerPos.Domain.Repositories
{
    public interface IReceiptRepository
    {
        void Create(ReceiptEntity receiptEntity);
        void Update(ReceiptEntity receiptEntity);
    }
}
