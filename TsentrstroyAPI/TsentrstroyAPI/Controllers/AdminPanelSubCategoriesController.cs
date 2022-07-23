using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.DatabaseActions;
using TsentrstroyAPI.Extensions;
using TsentrstroyAPI.Model;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("AdminPanel/SubCategories")]
    public class AdminPanelSubCategoriesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UploadFileHelper _uploadFileHelper;
        private BadRequestObjectResult _error = new BadRequestObjectResult("Incorrect data transmitted by the request");

        public AdminPanelSubCategoriesController(DatabaseContext databaseContext, UploadFileHelper uploadFileHelper)
        {
            _databaseContext = databaseContext;
            _uploadFileHelper = uploadFileHelper;
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategory() => Ok(await _databaseContext.SubCategories.FirstAsync());
        
        [HttpPost]
        public async Task<IActionResult> AddSubCategory([FromQuery] string categorySlug, [FromBody] SubCategory subCategory)
        {
            if (subCategory is null == true)
                return _error;

            await _databaseContext.Categories.LoadAsync();
            subCategory.Category = await _databaseContext.Categories.FirstOrDefaultAsync(c => c.Slug == categorySlug);
            
            await _databaseContext.SubCategories.AddAsync(subCategory);
            await _databaseContext.SaveChangesAsync();
            
            return Ok(subCategory);
        }

        [HttpPut]
        public async Task<IActionResult> EditSubCategory([FromQuery] int id, [FromBody] SubCategory updatedSubCategory)
        {
            if (updatedSubCategory is null == true)
                return _error;

            SubCategory subCategory = await _databaseContext.SubCategories.FirstAsync(s => s.Id == id);
            subCategory.Category = updatedSubCategory.Category;
            subCategory.Name = updatedSubCategory.Name;
            subCategory.IsActive = updatedSubCategory.IsActive;

            _databaseContext.SubCategories.Update(subCategory);
            await _databaseContext.SaveChangesAsync();

            return Ok($"Подкатегория {subCategory.Name} успешно изменена");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSubCategory([FromQuery] int id)
        {
            await _databaseContext.Categories.LoadAsync();
            SubCategory subCategory = await _databaseContext.SubCategories.FirstOrDefaultAsync(s => s.Id == id);

            if (subCategory is null == true)
                return BadRequest("При удалении подкатегории произошла ошибка: подкатегория не найдена");

            await SubCategoryDatabaseActions.Delete(subCategory, _databaseContext);

            await AddDefaultSubCategory(subCategory);

            return Ok("Подкатегория успешно удалена");
        }
        

        private async Task AddDefaultSubCategory(SubCategory subCategory)
        {
            Category category = await _databaseContext.Categories.FirstOrDefaultAsync(s => s == subCategory.Category);
            List<SubCategory> subCategories = await _databaseContext.SubCategories.Where(s => s.Category == category).ToListAsync();
            
            if(subCategories.Count != 0)
                return;
            
            SubCategory defaultSubCategory = new SubCategory()
            {
                Name = "Все товары",
                Category = category,
                IsActive = true
            };

            await _databaseContext.SubCategories.AddAsync(defaultSubCategory);
            await _databaseContext.SaveChangesAsync();
        }
    }
}