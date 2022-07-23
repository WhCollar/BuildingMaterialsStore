using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Extensions;
using TsentrstroyAPI.Services;


namespace TsentrstroyAPI.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("AdminPanel/Overview")]
    public class AdminPanelOverviewController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private readonly UploadFileHelper _uploadFileHelper;

        private const string _saveFolder = "OverviewBackgroundImages";
        
        public AdminPanelOverviewController(DatabaseContext databaseContext, UploadFileHelper uploadFileHelper)
        {
            _databaseContext = databaseContext;
            _uploadFileHelper = uploadFileHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _databaseContext.OverviewProducts.FirstOrDefaultAsync());

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            IFormCollection collection = HttpContext.Request.Form;
            await _databaseContext.Categories.LoadAsync();
            
            IFormFile image = HttpContext.GetFile("image");
            OverviewProduct overviewProduct = JsonConvert.DeserializeObject<OverviewProduct>(collection["overview"].ToString());

            if (string.IsNullOrEmpty(overviewProduct.Title) == true)
                return Ok();

            string slug = collection["categorySlug"].ToString();
            overviewProduct.Category = await _databaseContext.Categories.FirstOrDefaultAsync(c => c.Slug == slug);

            if (image is null == false)
                overviewProduct.ImageUrl = await _uploadFileHelper.UploadFile(image, _saveFolder);
            
            await _databaseContext.OverviewProducts.AddAsync(overviewProduct);
            await _databaseContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromQuery] int id)
        {
            await _databaseContext.Categories.LoadAsync();
            
            IFormCollection collection = HttpContext.Request.Form;

            IFormFile image = HttpContext.GetFile("image");
            OverviewProduct sourceOverview = await _databaseContext.OverviewProducts.FirstOrDefaultAsync(p => p.Id == id);
            OverviewProduct overviewProduct = JsonConvert.DeserializeObject<OverviewProduct>(collection["overview"].ToString());

            if (sourceOverview is null == true || sourceOverview.Category is null == true)
                return BadRequest("Overview не изменён. Не удалось найти категорию");
            
            bool editImage = bool.Parse(collection["imageIsChanged"]);
            
            UpdateOverview(sourceOverview, overviewProduct, collection);
            
            if (editImage == true)
                await UpdateImage(image, sourceOverview);
            
            _databaseContext.OverviewProducts.Update(sourceOverview);
            
            await _databaseContext.SaveChangesAsync();
            
            return Ok();
        }

        private void UpdateOverview(OverviewProduct sourceOverview, OverviewProduct overviewProduct, IFormCollection collection)
        {
            sourceOverview.Title = overviewProduct.Title;
            sourceOverview.Subtitle = overviewProduct.Subtitle;
            sourceOverview.Description = overviewProduct.Description;
            sourceOverview.Category = GetCategory(collection);
        }
        
        private async Task UpdateImage(IFormFile image, OverviewProduct sourceOverview)
        {
            if(image is null == true)
                return;
            
            _uploadFileHelper.RemoveFile(sourceOverview.ImageUrl);
            sourceOverview.ImageUrl = await _uploadFileHelper.UploadFile(image, _saveFolder);
        }
        
        private Category GetCategory(IFormCollection collection)
        {
            bool parsed = int.TryParse(collection["categoryId"], out int id);
            Category category = _databaseContext.Categories.FirstOrDefault(s => s.Id == id);
            return category;
        }
    }
}