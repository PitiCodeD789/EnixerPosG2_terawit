using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Service.Helpers;
using EnixerPos.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xamarin.Essentials;

namespace EnixerPos.Service.Services
{
    public class ReceiptService : BaseService,IReceiptService
    {
        private string serviceUrl = Helper.BaseUrl + "Receipt/";
        public List<ReceiptViewModel> GetReceiptByShiftId(int shiftId)
        {
            #region Mockup
            //return new List<ReceiptViewModel>(){ new ReceiptViewModel()
            //{
            //    Reference = "1-1234",
            //    ShiftId = 1,
            //    Store = "Enixer",
            //    ItemList = new List<OrderItemModel>() {
            //        new OrderItemModel()
            //        {
            //            ItemName = "water",
            //            ItemPrice = 50,
            //            Quantity = 1,
            //            OptionName = "Ice",
            //            OptionPrice = 5,
            //            IsDiscountPercentage = false,
            //            ItemDiscount = 5
            //        },
            //        new OrderItemModel()
            //        {
            //            ItemName = "Milk",
            //            ItemPrice = 50,
            //            Quantity = 4,
            //            OptionName = "Ice",
            //            OptionPrice = 5,
            //            IsDiscountPercentage = true,
            //            ItemDiscount = 5
            //        },
            //        new OrderItemModel()
            //        {
            //            ItemName = "Milk",
            //            ItemPrice = 50,
            //            Quantity = 5,
            //            OptionName = "Ice",
            //            OptionPrice = 0,
            //            IsDiscountPercentage = false,
            //            ItemDiscount = 0
            //        }
            //    },
            //    Discount = 10,
            //    IsDiscountPercentage = true,
            //    Total = 500,
            //    TotalDiscount = 10,
            //    CreateDateTime = DateTime.Now,
            //    PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Cash
            //},
            //new ReceiptViewModel()
            //{
            //    Reference = "1-1235",
            //    ShiftId = 1,
            //    Store = "Enixer",
            //    ItemList = new List<OrderItemModel>() {
            //        new OrderItemModel()
            //        {
            //            ItemName = "water",
            //            ItemPrice = 50,
            //            Quantity = 1,
            //            OptionName = "Ice",
            //            OptionPrice = 5,
            //            IsDiscountPercentage = false,
            //            ItemDiscount = 5
            //        },
            //        new OrderItemModel()
            //        {
            //            ItemName = "Milk",
            //            ItemPrice = 50,
            //            Quantity = 2,
            //            OptionName = "Ice",
            //            OptionPrice = 5,
            //            IsDiscountPercentage = true,
            //            ItemDiscount = 5
            //        },
            //        new OrderItemModel()
            //        {
            //            ItemName = "Milk",
            //            ItemPrice = 50,
            //            Quantity = 2,
            //            OptionName = "Ice",
            //            OptionPrice = 0,
            //            IsDiscountPercentage = false,
            //            ItemDiscount = 0
            //        }
            //    },
            //    Discount = 10,
            //    IsDiscountPercentage = true,
            //    Total = 100,
            //    TotalDiscount = 10,
            //    CreateDateTime = DateTime.Now,
            //    PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Credit

            //}
            //};
            #endregion

            try
            {
                string url = serviceUrl + "GetReceipts1DayByShiftId/" + shiftId.ToString();
                var result = Get<List<ReceiptViewModel>>(url).Result;
                if (result != null || result.IsError == System.Net.HttpStatusCode.OK)
                {
                    return result.Model;
                }
                return new List<ReceiptViewModel>();
            }
            catch (Exception)
            {

                return new List<ReceiptViewModel>();
            }


        }
    }
}
