using EnixerPos.Api.ViewModels.Sale;
using EnixerPos.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Service.Services
{
    public class ReceiptService : BaseService,IReceiptService
    {
        public List<ReceiptViewModel> GetReceipt()
        {
            return new List<ReceiptViewModel>(){ new ReceiptViewModel()
            {
                Reference = "1-1234",
                ShiftId = 1,
                Store = "Enixer",
                Pos = "POS1",
                ItemList = new List<OrderItemModel>() {
                    new OrderItemModel()
                    {
                        ItemName = "water",
                        ItemPrice = 50,
                        Quantity = 1,
                        OptionName = "Ice",
                        OptionPrice = 5,
                        IsDiscountPercentage = false,
                        ItemDiscount = 5
                    },
                    new OrderItemModel()
                    {
                        ItemName = "Milk",
                        ItemPrice = 50,
                        Quantity = 2,
                        OptionName = "Ice",
                        OptionPrice = 5,
                        IsDiscountPercentage = true,
                        ItemDiscount = 5
                    },
                    new OrderItemModel()
                    {
                        ItemName = "Milk",
                        ItemPrice = 50,
                        Quantity = 2,
                        OptionName = "Ice",
                        OptionPrice = 0,
                        IsDiscountPercentage = false,
                        ItemDiscount = 0
                    }
                },
                Discount = 10,
                IsDiscountPercentage = true,
                Total = 100,
                TotalDiscount = 10,
                CreateDateTime = DateTime.Now,
                PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Cash
            },
            new ReceiptViewModel()
            {
                Reference = "1-1235",
                ShiftId = 1,
                Store = "Enixer",
                Pos = "POS1",
                ItemList = new List<OrderItemModel>() {
                    new OrderItemModel()
                    {
                        ItemName = "water",
                        ItemPrice = 50,
                        Quantity = 1,
                        OptionName = "Ice",
                        OptionPrice = 5,
                        IsDiscountPercentage = false,
                        ItemDiscount = 5
                    },
                    new OrderItemModel()
                    {
                        ItemName = "Milk",
                        ItemPrice = 50,
                        Quantity = 2,
                        OptionName = "Ice",
                        OptionPrice = 5,
                        IsDiscountPercentage = true,
                        ItemDiscount = 5
                    },
                    new OrderItemModel()
                    {
                        ItemName = "Milk",
                        ItemPrice = 50,
                        Quantity = 2,
                        OptionName = "Ice",
                        OptionPrice = 0,
                        IsDiscountPercentage = false,
                        ItemDiscount = 0
                    }
                },
                Discount = 10,
                IsDiscountPercentage = true,
                Total = 100,
                TotalDiscount = 10,
                CreateDateTime = DateTime.Now,
                PaymentType = Api.ViewModels.Enixer_Enumerations.EP_PaymentTypeEnum.Credit

            }
            };
        }
    }
}
