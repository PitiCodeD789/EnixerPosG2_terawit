using EnixerPos.Api.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnixerPos.Domain.Interfaces
{
    public interface IProductService
    {
        ItemsViewModel GetItems(string user);
        CategoriesViewModel GetCategories(string audience);
        DiscountsViewModel GetDiscounts(string audience);
        ResultViewModel AddItem(string audience, ItemModel item);
        ResultViewModel AddCategory(string audience, CategoryModel category);
        ResultViewModel AddDiscount(string audience, DiscountModel discount);
        ItemModel GetItem(string audience, int itemId);
        CategoryModel GetCategory(string audience, int categoryId);
        DiscountModel GetDiscount(string audience, int discountId);
        ResultViewModel UpdateCategory(string audience, CategoryModel command);
        ResultViewModel UpdateDiscount(string audience, DiscountModel command);
        ResultViewModel UpdateItem(string audience, ItemModel command);
    }
}
