using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Sale;

namespace EnixerPos.Domain.Interfaces
{
    public interface ISaleService
    {
        ReceiptViewModel CreateReceipt(PaymentCommand payment);
        bool CheckPayment(string paymentRef);
    }
}
