using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Data.AdminPanelJsons.Product;
using TsentrstroyAPI.DatabaseActions;
using TsentrstroyAPI.Extensions;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("AdminPanel/Products")]
    public class AdminPanelProductController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UploadFileHelper _uploadFileHelper;
        private readonly IMapper _mapper;
        
        public AdminPanelProductController(DatabaseContext databaseContext, UploadFileHelper uploadFileHelper, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _uploadFileHelper = uploadFileHelper;

            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool fillProducts)
        {
            List<Category> categories = await _databaseContext.Categories.ToListAsync();
            List<CategoryJsonStruct> items = new List<CategoryJsonStruct>();
            
            foreach (var category in categories)
            {
                CategoryJsonStruct categoryJson = _mapper.Map<CategoryJsonStruct>(category);

                List<SubCategory> subCategories = await _databaseContext.SubCategories.Where(s => s.Category == category).ToListAsync();
                List<SubCategoryJsonStruct> jsonSubCategories = new List<SubCategoryJsonStruct>();

                foreach (var subCategory in subCategories)
                    jsonSubCategories.Add(await AddSubCategory(subCategory, fillProducts));
                
                categoryJson.SubCategories = jsonSubCategories;
                
                items.Add(categoryJson);
            }
           
            return Ok(items);
        }

        private async Task<SubCategoryJsonStruct> AddSubCategory(SubCategory subCategory, bool fillProducts)
        {
            SubCategoryJsonStruct jsonSubCategory = _mapper.Map<SubCategoryJsonStruct>(subCategory);

            if (fillProducts == true)
            {
                jsonSubCategory.Products = await _databaseContext.Products.AsNoTracking().Where(p => p.SubCategory == subCategory).ToListAsync();
                jsonSubCategory.Products = jsonSubCategory.Products.OrderBy(p => p.Id).ToList();
            }
            else
            {
                jsonSubCategory.Products = null;
            }

            return jsonSubCategory;
        }
        

        [HttpPost]
        public async Task<IActionResult> AddProduct()
        {
            IFormCollection collection = HttpContext.Request.Form;
            
            IFormFile image = HttpContext.GetFile("image");
            Product product = JsonConvert.DeserializeObject<Product>(collection["product"].ToString());
            SubCategory subCategory = GetSubCategory(collection);
            
            product.ImageUrl = await _uploadFileHelper.UploadFile(image, "ProductsImages");
            product.SubCategory = subCategory;
            
            await _databaseContext.Products.AddAsync(product);
            await _databaseContext.SaveChangesAsync();

            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromQuery] int id)
        {
            IFormCollection collection = HttpContext.Request.Form;

            IFormFile image = HttpContext.GetFile("image");
            Product sourceProduct = await _databaseContext.Products.FirstAsync(p => p.Id == id);
            Product product = JsonConvert.DeserializeObject<Product>(collection["product"].ToString());

            UpdateProductValue(sourceProduct, product, collection);
            if (sourceProduct.SubCategory is null == true)
                return BadRequest("Товар не изменён. Не удалось найти подкатегорию");
            
            bool editImage = bool.Parse(collection["imageIsChanged"]);

            if (editImage == true)
                await UpdateImage(image, sourceProduct);

            _databaseContext.Products.Update(sourceProduct);
            await _databaseContext.SaveChangesAsync();
            
            return Ok();
        }
        
        private void UpdateProductValue(Product sourceProduct, Product product, IFormCollection collection)
        {
            sourceProduct = _mapper.Map<Product>(product);
            sourceProduct.SubCategory = GetSubCategory(collection);
        }
        
        private SubCategory GetSubCategory(IFormCollection collection)
        {
            bool parsed = int.TryParse(collection["subCategoryId"], out int id);
            SubCategory subCategory = _databaseContext.SubCategories.FirstOrDefault(s => s.Id == id);
            return subCategory;
        }

        private async Task UpdateImage(IFormFile image, Product sourceProduct)
        {
            if(image is null == true)
                return;

            _uploadFileHelper.RemoveFile(sourceProduct.ImageUrl);
            sourceProduct.ImageUrl = await _uploadFileHelper.UploadFile(image, "ProductsImages");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] int id)
        {
            Product product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null == true)
                return BadRequest("При удалении продукта произошла ошибка: товар не найден");

            await ProductDatabaseActions.Delete(product, _databaseContext);

            return Ok("Товар успешно удалён");
        }
    }
}