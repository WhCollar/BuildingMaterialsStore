using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;
using TsentrstroyAPI.Model;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoreAtProductController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public MoreAtProductController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int id)
        {
            await _databaseContext.AreasOfUseTabs.LoadAsync();
            await _databaseContext.FeatureListTabs.LoadAsync();
            await _databaseContext.Products.LoadAsync();

            List<MoreAtProduct> moreAtProducts = await _databaseContext.MoreAtProducts.OrderBy(m => m.Id).Where(m => m.Product.Id == id).ToListAsync();
            MoreAtProduct moreAtProduct = moreAtProducts.LastOrDefault();

            if (moreAtProduct is null == true)
                return NotFound();
            
            if (moreAtProduct.AreasOfUse is null == true)
                moreAtProduct.AreasOfUse = new List<AreasOfUseTab>();
            
            if (moreAtProduct.FeatureList is null == true)
                moreAtProduct.FeatureList = new List<FeatureListTab>();

            return Ok(moreAtProduct);
        }
    }
}