using AutoMapper;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Domain.DtoModels;
using EnixerPos.Domain.Entities;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EnixerPos.Domain.Services
{
    public class ProductService : IProductService
    {
        IItemRepository _itemRepository;
        ICategoryRepository _categoryRepository;
        IDiscountRepository _discountRepository;
        IStoreRepository _storeRepository;
        IMapper _mapper;

        public ProductService(IItemRepository itemRepository, ICategoryRepository categoryRepository, IDiscountRepository discountRepository, IStoreRepository storeRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _categoryRepository = categoryRepository;
            _discountRepository = discountRepository;
            _storeRepository = storeRepository;
            _mapper = mapper;
        }

        #region Add
        public ResultViewModel AddCategory(string audience, CategoryModel category)
        {
            ResultViewModel viewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map category to categoryDto
                CategoryDto categoryDto = new CategoryDto();
                categoryDto.Name = category.Name;
                categoryDto.Color = category.Color;
                bool addResult = _categoryRepository.AddCategory(storeId, categoryDto);
                viewModel.IsError = !addResult;

                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }

        public ResultViewModel AddDiscount(string audience, DiscountModel discount)
        {
            ResultViewModel viewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map discount to DiscountDto
                DiscountDto discountDto = new DiscountDto();
                bool addResult = _discountRepository.AddDiscount(storeId, discountDto);
                viewModel.IsError = !addResult;

                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }

        public ResultViewModel AddItem(string audience, ItemModel item)
        {
            ResultViewModel viewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map item to itemDto
                ItemDto itemDto = new ItemDto(); 
                bool addResult = _itemRepository.AddItem(storeId, itemDto);
                viewModel.IsError = !addResult;

                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }
        #endregion

        #region GetAll
        public CategoriesViewModel GetCategories(string audience)
        {
            CategoriesViewModel viewModel = new CategoriesViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                List<CategoryDto> categories = _categoryRepository.GetCategoriesByStoreId(storeId);

                viewModel.Categories = Map<CategoryDto,CategoryModel>(categories);
                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }

        public DiscountsViewModel GetDiscounts(string audience)
        {
            DiscountsViewModel viewModel = new DiscountsViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                List<DiscountDto> discounts = _discountRepository.GetDiscountsByStoreId(storeId);

                viewModel.Discounts = Map<DiscountDto,DiscountModel>(discounts);
                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }

        public ItemsViewModel GetItems(string audience)
        {
            ItemsViewModel viewModel = new ItemsViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                List<ItemDto> items = _itemRepository.GetItemsByStoreId(storeId);
                foreach (var item in items)
                {
                    if (item.CategoryId == null || item.CategoryId == 0)
                        item.CategoryName = "Undefined";
                    else
                    {
                        string categoryName = _categoryRepository.GetCategory(storeId, item.CategoryId).Name;
                        item.CategoryName = categoryName;
                    }
                }
                var tempItem = Map<ItemDto,ItemModel>(items);

                viewModel.Items = tempItem;
                return viewModel;
            }
            catch (Exception e)
            {
                viewModel.Message = e.Message;
                viewModel.IsError = true;
                return viewModel;
            }
        }
        #endregion

        #region Get
        public CategoryModel GetCategory(string audience, int categoryId)
        {
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                CategoryDto categoryDto = _categoryRepository.GetCategory(storeId, categoryId);

                //TODO Map CategoryDto to CategoryModel
                CategoryModel category = new CategoryModel();

                return category;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DiscountModel GetDiscount(string audience, int discountId)
        {
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                DiscountDto discountDto = _discountRepository.GetDiscount(storeId, discountId);

                //TODO Map DiscountDto to DiscountModel
                DiscountModel discount = new DiscountModel();

                return discount;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public ItemModel GetItem(string audience, int itemId)
        {
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                ItemDto itemDto = _itemRepository.GetItem(storeId, itemId);

                //TODO Map ItemDto to ItemModel
                ItemModel item = new ItemModel();

                return item;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        #endregion

        #region Update
        public ResultViewModel UpdateCategory(string audience, CategoryModel category)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map category to categoryDto
                CategoryDto categoryDto = new CategoryDto();
                bool result = _categoryRepository.Update(storeId, categoryDto);

                resultViewModel.IsError = !result;
                return resultViewModel;
            }
            catch (Exception e)
            {
                resultViewModel.IsError = true;
                resultViewModel.Message = e.Message;
                return resultViewModel;
            }
        }

        public ResultViewModel UpdateDiscount(string audience, DiscountModel discount)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map category to categoryDto
                DiscountDto discountDto = new DiscountDto();
                bool result = _discountRepository.Update(storeId, discountDto);

                resultViewModel.IsError = !result;
                return resultViewModel;
            }
            catch (Exception e)
            {
                resultViewModel.IsError = true;
                resultViewModel.Message = e.Message;
                return resultViewModel;
            }
        }

        public ResultViewModel UpdateItem(string audience, ItemModel item)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            try
            {
                int storeId = _storeRepository.GetStoreIdByEmail(audience);

                if (storeId == 0)
                    throw new FileNotFoundException();

                //TODO Map ItemModel to ItemDto
                ItemDto itemDto = new ItemDto();
                bool result = _itemRepository.Update(storeId, itemDto);

                resultViewModel.IsError = !result;
                return resultViewModel;
            }
            catch (Exception e)
            {
                resultViewModel.IsError = true;
                resultViewModel.Message = e.Message;
                return resultViewModel;
            }
        }
        #endregion


        public H Map<T, H>(T post)
            where T : class
            where H : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, H>());
            var mapper = new Mapper(config);
            H postDTO = mapper.Map<H>(post);
            return postDTO;
        }

        public List<H> Map<T, H>(List<T> model) //T - TypeIn, H - TypeOut
            where T : class
            where H : class
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<T, H>());
            var mapper = new Mapper(config);
            List<H> DTOs = mapper.Map<List<H>>(model);
            return DTOs;
        }
    }
}
