using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.DatabaseActions;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.Controllers
{
    [ApiController, Authorize(Roles = "Administrator")]
    [Route("AdminPanel/MoreAtProduct")]
    public class AdminPanelMoreAtProduct : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public AdminPanelMoreAtProduct(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromQuery] int productId, [FromBody] MoreAtProduct moreAtProduct)
        {
            await _databaseContext.AreasOfUseTabs.LoadAsync();
            await _databaseContext.FeatureListTabs.LoadAsync();
            await _databaseContext.Products.LoadAsync();
            
            moreAtProduct.Product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            await _databaseContext.MoreAtProducts.AddAsync(moreAtProduct);
            await _databaseContext.SaveChangesAsync();

            return Ok("Информация для страницы товара успешно обновлена");
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromQuery] int productId, [FromBody] MoreAtProduct moreAtProduct)
        {
            MoreAtProduct source = await _databaseContext.MoreAtProducts.FirstOrDefaultAsync(m => m.Product.Id == productId);
            
            if (source == null)
                return BadRequest("Не удалось обновить информацию для страницы товара");

            await MoreAtProductDatabaseActions.DeleteAreasOfUse(source, _databaseContext);
            await MoreAtProductDatabaseActions.DeleteFeatureList(source, _databaseContext);
            
            _databaseContext.Remove(source);
            await _databaseContext.SaveChangesAsync();

            moreAtProduct.Product = await _databaseContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            
            await _databaseContext.MoreAtProducts.AddAsync(moreAtProduct);
            await _databaseContext.SaveChangesAsync();
            
            return Ok("Информация для страницы товара успешно обновлена");
        }
    }
}