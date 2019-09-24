using EnixerPos.Api.ViewModels.Sale;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Service.Interfaces
{
    public interface IReceiptService
    {
        List<ReceiptViewModel> GetReceiptByShiftId(int shiftId);

    }
}
