using Backend.Context;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ShopController(DatabaseContext context)
        {
            _context = context;
        }
        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Backend.Entities.Shop>> GetShopByID([FromRoute] long id)
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(q => q.Id == id);
            if (shop is null)
            {
                return NotFound("Product Not Found");
            }
            return Ok(shop);
        }
        [EnableCors]
        [HttpGet]
        public async Task<ActionResult<List<Backend.Entities.Shop>>> GetShops()
        {
            var shops = await _context.Shops.ToListAsync();
            
            
            return Ok(shops);
        }
        [EnableCors]
        [HttpGet]
        [Route("arr")]
        public async Task<ActionResult<List<long>>> GetShopsIDs()
        {
            var shops = await _context.Shops.ToListAsync();
            List<long> ids = new();
            foreach(Backend.Entities.Shop shop in shops)
            {
                ids.Add(shop.Id);
            }
            return Ok(ids);
        }
    }
}
