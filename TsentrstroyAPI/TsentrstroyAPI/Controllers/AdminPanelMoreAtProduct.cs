using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TsentrstroyAPI.Data;
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
            await _databaseContext.AreasOfUseTabs.AsNoTracking().LoadAsync();
            await _databaseContext.FeatureListTabs.AsNoTracking().LoadAsync();
            await _databaseContext.Products.AsNoTracking().LoadAsync();

            MoreAtProduct source = await _databaseContext.MoreAtProducts.AsNoTracking().FirstOrDefaultAsync(m => m.Product.Id == productId);

            if (source is null == true)
                return BadRequest("Не удалось обновить информацию для страницы товара");

            await MoreAtProductDatabaseActions.DeleteAreasOfUse(source, _databaseContext);
            await MoreAtProductDatabaseActions.DeleteFeatureList(source, _databaseContext);

            source.AreasOfUse = null;
            source.FeatureList = null;

            source.Gost = moreAtProduct.Gost;
            source.Advantages = moreAtProduct.Advantages;
            source.Compound = moreAtProduct.Compound;
            source.FeatureList = moreAtProduct.FeatureList;
            source.AreasOfUse = moreAtProduct.AreasOfUse;
            source.Notes = moreAtProduct.Notes;
            source.Product = await _databaseContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == productId);
            
            _databaseContext.MoreAtProducts.Update(source);
            await _databaseContext.SaveChangesAsync();

            return Ok("Информация для страницы товара успешно обновлена");
        }
    }
}