using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Domain.DtoModels.Sale;

namespace EnixerPos.Domain.Interfaces
{
    public interface ISaleService
    {
        ReceiptDto CreateReceipt(PaymentCommand payment);
        bool CheckPayment(string paymentRef);
    }
}
