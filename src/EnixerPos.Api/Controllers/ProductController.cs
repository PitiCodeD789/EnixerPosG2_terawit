using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EnixerPos
    
    .Api.ViewModels.Product;
using EnixerPos.Api.ViewModels.Product;
using EnixerPos.Domain.Interfaces;
using EnixerPos.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnixerPos.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region GetAll
        [HttpGet("GetItems")]
        public IActionResult GetItems()
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ItemsViewModel viewModel = _productService.GetItems(audience);

            if (viewModel.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(viewModel);
        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategories()
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            CategoriesViewModel viewModel = _productService.GetCategories(audience);

            if (viewModel.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(viewModel);
        }

        [HttpGet("GetDiscounts")]
        public IActionResult GetDiscounts()
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            DiscountsViewModel viewModel = _productService.GetDiscounts(audience);

            if (viewModel.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(viewModel);
        }
        #endregion

        #region Add
        [HttpPost("AddItem")]
        public IActionResult AddItem([FromBody]ItemModel item)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.AddItem(audience, item);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory([FromBody]CategoryModel category)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.AddCategory(audience, category);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }

        [HttpPost("AddDiscount")]
        public IActionResult AddDiscount([FromBody]DiscountModel discount)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.AddDiscount(audience, discount);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }
        #endregion

        #region Get
        [HttpGet("GetItem/{itemId}")]
        public IActionResult GetItem(int itemId)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ItemModel item = _productService.GetItem(audience, itemId);

            if (item == null)
                return BadRequest(new { Message = "Error" });

            return Ok(item);
        }

        [HttpGet("GetCategory/{categoryId}")]
        public IActionResult GetCategory(int categoryId)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            CategoryModel category = _productService.GetCategory(audience, categoryId);

            if (category == null)
                return BadRequest(new { Message = "Error" });

            return Ok(category);
        }

        [HttpGet("GetDiscount/{discountId}")]
        public IActionResult GetDiscount(int discountId)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            DiscountModel discount = _productService.GetDiscount(audience, discountId);

            if (discount == null)
                return BadRequest(new { Message = "Error" });

            return Ok(discount);
        }
        #endregion

        #region Update
        [HttpPost("UpdateCategory")]
        public IActionResult UpdateCategory([FromBody]CategoryModel category)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.UpdateCategory(audience, category);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }

        [HttpPost("UpdateDiscount")]
        public IActionResult UpdateDiscount([FromBody]DiscountModel discount)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.UpdateDiscount(audience, discount);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }

        [HttpPost("UpdateItem")]
        public IActionResult UpdateItem([FromBody]ItemModel item)
        {
            var audience = User.Claims.FirstOrDefault(c => c.Type == "aud").Value;
            var user = User.Claims.FirstOrDefault(c => c.Type == "user").Value;

            ResultViewModel result = _productService.UpdateItem(audience, item);

            if (result.IsError)
                return BadRequest(new { Message = "Error" });

            return Ok(result);
        }
        #endregion
    }
}