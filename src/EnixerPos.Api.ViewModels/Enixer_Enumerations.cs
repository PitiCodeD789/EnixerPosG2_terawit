using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Api.ViewModels
{
    public static class Enixer_Enumerations
    {
        /// <summary>
        /// Cash = 0, Debit = 1,Credit = 2,Qr = 3
        /// </summary>
        public enum EP_PaymentTypeEnum
        {
            Cash = 0,
            Debit = 1,
            Credit = 2,
            Qr = 3

        }
        public enum ManageCashStatus
        {
            PayIn,
            PayOut
        }
    }
}
