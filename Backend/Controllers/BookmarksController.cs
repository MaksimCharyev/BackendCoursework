using Backend.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Backend.Entities;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public BookmarksController(DatabaseContext context)
        {
            _context = context;
        }
        [EnableCors]
        [HttpPost]
        [Route("{id}")]
        public async Task<IActionResult> CreateBookmark([FromRoute] long id, [FromBody] Backend.Entities.DTO.IdProductDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(q => q.Id == id);
            var product = await _context.Products.FirstOrDefaultAsync(t => t.Id == dto.Id);
            if (user == null) return NotFound("User not found!");
            if (product == null) return NotFound("Product not found!");
            var bookmark = new Backend.Entities.UserMarkedProduct();
            bookmark.User = user;
            bookmark.Product = product;
            bookmark.UserId = user.Id;
            bookmark.ProductId = dto.Id;
            _context.UserMarkedProducts.Add(bookmark);            
            //await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return Ok("Product of user saved");
        }
        [EnableCors]
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<List<Backend.Entities.ProductHasPrice>>> GetMarkedProduct([FromRoute] long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(q => q.Id == id);
            if(user is null)
            {
                return NotFound("User not found!");
            }
            
            var products = _context.UserMarkedProducts
    .Where(bookmark => bookmark.UserId == id).ToList();
            //var json = JsonSerializer.Serialize(products, options);

            return Ok(products);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMarkedProduct([FromRoute] long id, [FromBody] Backend.Entities.DTO.IdProductDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(q => q.Id == id);
            if (user is null)
            {
                return NotFound("User not found!");
            }
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == dto.Id);
            if(product is null)
            {
                return NotFound("Product not found!");
            }
            var bookmark = _context.UserMarkedProducts
    .FirstOrDefault(b => b.UserId == id && b.ProductId == dto.Id);

            if (bookmark == null)
            {
                return NotFound("There is no bookmarks!");
                
            }
            _context.UserMarkedProducts.Remove(bookmark);
            await _context.SaveChangesAsync();
            return Ok("Bookmark has removed");
        }
    }
}


