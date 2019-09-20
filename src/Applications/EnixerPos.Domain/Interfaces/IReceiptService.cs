using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Domain.DtoModels.Sale;

namespace EnixerPos.Domain.Interfaces
{
    public interface IReceiptService
    {
        List<ReceiptDto> GetReceiptsByDate(DateTime date);
    }
}
