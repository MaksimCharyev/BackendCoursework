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
    public class PricesController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public PricesController(DatabaseContext context)
        {
            _context = context;
        }
        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Backend.Entities.ProductHasPrice>> GetPriceByID([FromRoute] long id)
        {
            //var product = await _context.Products.FirstOrDefaultAsync(q => q.Id == id);
            var prices = await _context.ProductHasPrice.Where(x => x.ProductId == id).ToListAsync();
            if (prices is null)
            {
                return NotFound("Prises Not Found");
            }
            return Ok(prices);
        }
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> PostPriceProduct([FromRoute] long id, [FromBody] Backend.Entities.DTO.PriceDTO dto )
        {
            var shop = await _context.Shops.FirstOrDefaultAsync(t => t.Id == dto.Shop);
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            var price = new Backend.Entities.ProductHasPrice();
            price.ShopId = dto.Shop;
            price.Shop = shop;
            price.Product = product;
            price.Price = dto.Price;
            price.Date = dto.DateTime;
            _context.ProductHasPrice.Add(price);
            await _context.SaveChangesAsync();
            return Ok("Price added");
        }
    }
}
