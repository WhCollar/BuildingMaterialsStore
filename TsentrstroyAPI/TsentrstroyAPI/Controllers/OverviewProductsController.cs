using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TsentrstroyAPI.Data;

namespace TsentrstroyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OverviewProductsController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public OverviewProductsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get([FromRoute] string slug)
        {
            Category category = _databaseContext.Categories.AsNoTracking().FirstOrDefault(c => c.Slug == slug);

            if (category is null == true)
                return BadRequest("Некорректый slug");
            
            OverviewProduct overviewProduct = await _databaseContext.OverviewProducts.AsNoTracking().FirstOrDefaultAsync(o => o.Category == category);
            
            return Ok(overviewProduct);
        }
    }
}