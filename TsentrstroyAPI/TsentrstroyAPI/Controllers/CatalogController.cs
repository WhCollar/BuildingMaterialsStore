using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Data.AdminPanelJsons.Product;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        public DatabaseContext _databaseContext;

        public CatalogController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            List<Category> categories = await _databaseContext.Categories.AsNoTracking().ToListAsync();
            
            return Ok(categories);
        }

        [HttpGet("Category")]
        public async Task<IActionResult> CategoryById([FromQuery] int id)
        {
            Category category = await _databaseContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            return Ok(category);
        }

        [HttpGet("SubCategories/All")]
        public async Task<IActionResult> GetAllSubCategories()
        {
            await _databaseContext.Categories.LoadAsync();
            List<SubCategory> subCategories = await _databaseContext.SubCategories.ToListAsync();

            return Ok(subCategories);
        }

        [HttpGet("Sub")]
        public async Task<IActionResult> GetSubCategories([FromQuery] string slug)
        {
            await _databaseContext.SubCategories.AsNoTracking().LoadAsync();
            List<SubCategory> subCategories = _databaseContext.SubCategories.AsNoTracking().Where(s => s.Category.Slug == slug && s.Name != "Все товары").ToList();
            return Ok(subCategories);
        }

        [HttpGet("{slug}")]
        public async Task<IActionResult> GetProductsBySlug([FromRoute] string slug)
        {
            Category category = await _databaseContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Slug == slug);

            if (category is null == true)
                return BadRequest($"Товаров в категории с slug'ом {slug} не найдено");
            
            List<SubCategory> subCategories = _databaseContext.SubCategories.Where(s => s.Category.Id == category.Id).ToList();

            List<Product> resultProducts = new List<Product>();
            foreach (var subCategory in subCategories)
            {
                List<Product> selectedProducts = await _databaseContext.Products.Where(p => p.SubCategory.Id == subCategory.Id).OrderBy(p => p.Id).ToListAsync();
                
                foreach (var selectedProduct in selectedProducts)
                {
                    resultProducts.Add(selectedProduct);
                }
            }
            
            return Ok(resultProducts);
        }

        [HttpGet("Products")]
        public async Task<IActionResult> GetProductById([FromQuery] int id)
        {
            await _databaseContext.SubCategories.LoadAsync();
            Product product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null == true)
                return BadRequest($"Товар с id {id} не найден");

            return Ok(product);
        }

        [HttpGet("Products/All")]
        public async Task<IActionResult> All()
        {
            await _databaseContext.SubCategories.LoadAsync();
            List<Product> products = await _databaseContext.Products.ToListAsync();
            return Ok(products.OrderBy(p => p.Id));
        }
    }
}