using Backend.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Backend.Entities.Shop>> GetShopByID([FromRoute] long id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(q => q.Id == id);
            if (category is null)
            {
                return NotFound("Category Not Found");
            }
            return Ok(category);
        }

        [HttpGet]
        public async Task<ActionResult<List<Backend.Entities.Shop>>> GetShops()
        {
            var categories = await _context.Categories.ToListAsync();
            if (categories is null)
            {
                return NotFound("Categories Not Found");
            }
            return Ok(categories);
        }
    }
}
