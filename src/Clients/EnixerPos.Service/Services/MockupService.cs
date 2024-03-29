﻿using System;
using System.Collections.Generic;
using System.Text;
using EnixerPos.Api.ViewModels.Product;
using Newtonsoft.Json;

namespace EnixerPos.Service.Services
{
    public class MockupService
    {
        public ItemsViewModel GetItems()
        {
            ItemsViewModel items = new ItemsViewModel()
            {
                Items = new List<ItemModel>()
                {
                    new ItemModel()
                    {
                        Name = "Espresso",
                        CategoryName = "Drinks",
                        Option1 = "Hot",
                        Option1Price = 0,
                        Color = null,
                        Price = 50,
                        Id = 1,
                        CreateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333"),
                        UpdateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333")
                    },
                    new ItemModel()
                    {
                        Name = "Americano",
                        CategoryName = "Drinks",
                        Option1 = "Hot",
                        Option1Price = 0,
                        Option2 = "Iced",
                        Option2Price = 5,
                        Option3 = "Shake",
                        Option3Price = 15,
                        Color = null,
                        Price = 50,
                        Id = 2,
                        CreateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333"),
                        UpdateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333")
                    },
                    new ItemModel()
                    {
                        Name = "Spaghetti Carbonara",
                        CategoryName = "Foods",
                        Color = null,
                        Price = 150,
                        Id = 3,
                        CreateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333"),
                        UpdateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333")
                    },
                }
            };
            //List<string> drinks = new List<string>() { "Macchiato", "Cappuchino"};
            //for (int i = 0; i < 20; i++)
            //{
            //    items.Items.Add(new ItemModel()
            //    {
            //        Name = "Espresso "+i,
            //        CategoryName = "Drinks",
            //        Color = null,
            //        Price = 50+i,
            //        Id = 3+i,
            //        CreateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333"),
            //        UpdateDateTime = Convert.ToDateTime("2019-09-21T05:41:33.0033333")
            //    });
            //}
            return items;
        }

        public List<CategoryModel> GetCategories()
        {
            List<CategoryModel> categories = new List<CategoryModel>()
            {
                new CategoryModel
                {
                    Name = "Drinks",
                    Color = "",
                },
                new CategoryModel
                {
                    Name = "Foods",
                    Color = "",
                },
                new CategoryModel
                {
                    Name = "Snacks",
                    Color = "",
                },
            };

            return categories;
        }


    }
}
