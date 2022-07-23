using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cyrillic2Slug;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.DatabaseActions;
using TsentrstroyAPI.Model;
using TsentrstroyAPI.Services;

namespace TsentrstroyAPI.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("AdminPanel/Categories")]
    public class AdminPanelCategoriesController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UploadFileHelper _uploadFileHelper;

        public AdminPanelCategoriesController(DatabaseContext databaseContext, UploadFileHelper uploadFileHelper)
        {
            _databaseContext = databaseContext;
            _uploadFileHelper = uploadFileHelper;
        }
        
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            string slug = category.Name.ToSlug();
            category.Slug = slug;
            
            await _databaseContext.Categories.AddAsync(category);
            await _databaseContext.SaveChangesAsync();

            await AddDefaultSubCategory(category);

            return Ok(category);
        }

        private async Task AddDefaultSubCategory(Category category)
        {
            SubCategory defaultSubCategory = new SubCategory();
            defaultSubCategory.Name = "Все товары";
            defaultSubCategory.IsActive = true;
            defaultSubCategory.Category = category;
            

                await _databaseContext.SubCategories.AddAsync(defaultSubCategory);
            await _databaseContext.SaveChangesAsync();
        }

        [HttpPut]
        public async Task<IActionResult> EditCategory([FromQuery] int id, [FromBody] Category updatedCategory)
        {
            Category category = await _databaseContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null == true)
                return BadRequest($"Категория с id {id} не найдена");
            
            category.Name = updatedCategory.Name;
            category.Slug = updatedCategory.Name.ToSlug();

            _databaseContext.Categories.Update(category);
            await _databaseContext.SaveChangesAsync();

            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] int id)
        {
            Category category = await _databaseContext.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category is null == true)
                return BadRequest("При удалении категории произошла ошибка: категория не найдена");

            await CategoryDatabaseActions.Delete(category, _databaseContext);

            return Ok("Категория успешно удалена");
        }
    }
}