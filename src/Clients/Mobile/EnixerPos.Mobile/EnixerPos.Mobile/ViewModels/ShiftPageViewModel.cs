using EnixerPos.Api.ViewModels.Shifts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Mobile.ViewModels
{
    public class ShiftPageViewModel
    {
        public GetShiftViewModel getShiftView { get; set; }
        public ShiftPageViewModel()
        {
            getShiftView = new GetShiftViewModel
            {
                CreateDateTime = DateTime.Now,
                UpdateDateTime = DateTime.Now.AddHours(12),
                StartingCash = 5005,
                CashPayment = 500,
                Cash = 1500,
                CashRefunds = 0,
                CreditCard = 500,
                DebitCard = 1500,
                Discount = 250,
                Grosssales = 550,
                Paidin = 200,
                Taxes = 145.60m
            };
   
        }
    }
}
