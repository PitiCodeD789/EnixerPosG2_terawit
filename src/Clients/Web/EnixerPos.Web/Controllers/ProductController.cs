using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Register");
            }
            return View();
        }

        public IActionResult EditItem()
        {
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Register");
            }
            ViewBag.Email = email;
            List<ItemModel> items = _productService.GetItems(email).Items;
            List<ItemWebModel> itemWebs = items.Select(x => new ItemWebModel()
            {
                Name = x.Name,
                CategoryName = x.CategoryName,
                Color = x.Color,
                Price = x.Price,
                Cost = x.Cost,
                Option1 = x.Option1,
                Option1Price = x.Option1Price,
                Option2 = x.Option2,
                Option2Price = x.Option2Price,
                Option3 = x.Option3,
                Option3Price = x.Option3Price,
                Option4 = x.Option4,
                Option4Price = x.Option4Price,
                Option5 = x.Option5,
                Option5Price = x.Option5Price,
                Option6 = x.Option6,
                Option6Price = x.Option6Price
            }).ToList();
            ViewData["Items"] = itemWebs;
            return View();
        }

        public IActionResult EditCategory()
        {
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Register");
            }
            ViewBag.Email = email;
            List<CategoryModel> categories = _productService.GetCategories(email).Categories;
            List<CategoriesWebModel> categoriesWebs = categories.Select(x => new CategoriesWebModel()
            {
                Name = x.Name,
                Color = x.Color,
                StoreId = x.StoreId,
                CountItem = x.CountItem
            }).ToList();
            ViewData["Categories"] = categoriesWebs;
            return View();
        }

        public IActionResult EditDiscount()
        {
            string email = HttpContext.Session.GetString("Email");
            if (String.IsNullOrEmpty(email))
            {
                return RedirectToAction("Index", "Register");
            }
            ViewBag.Email = email;
            List<DiscountModel> discounts = _productService.GetDiscounts(email).Discounts;
            List<DiscountWebModel> discountWebs = discounts.Select(x => new DiscountWebModel()
            {
                DiscountName = x.DiscountName,
                Amount = x.Amount,
                IsPercentage = x.IsPercentage,
                StoreId = x.StoreId
            }).ToList();
            ViewData["Discounts"] = discountWebs;
            return View();
        }
    }
}