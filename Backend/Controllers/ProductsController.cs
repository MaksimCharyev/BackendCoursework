using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace Backend.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ProductsController(DatabaseContext context)
        {
            _context = context;
        }
        //Create
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Backend.Entities.DTO.ProductDTO dto)
        {
            var newProduct = new Backend.Entities.Product();
            newProduct.Name = dto.Name;
            newProduct.Description = dto.Description;
            newProduct.ImageURL = dto.ImageURL;
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return Ok("Product saved");
        }
        //Read
        [HttpGet]
        public async Task<ActionResult<List<Backend.Entities.Product>>> GetAllProducts()
        {
            var products = await _context.Products.Include(u => u.Categories).Include(u => u.ProductHasPrices).ToListAsync();
            return Ok(products);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Backend.Entities.Product>> GetProductByID([FromRoute] long id)
        {
            var product = await _context.Products.Include(u => u.Categories).Include(u => u.ProductHasPrices).FirstOrDefaultAsync(q => q.Id == id);
            if(product is null)
            {
                return NotFound("Product Not Found");
            }
            return Ok(product);
        }

        //Update
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromBody] Backend.Entities.DTO.ProductDTO dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(q => q.Id == id);
            if (product is null)
            {
                return NotFound("Product Not Found");
            }
            product.Name = dto.Name;
            product.Description = dto.Description;
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok("Product Updated");
        }
        
        //Delete
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(q => q.Id == id);
            if (product is null)
            {
                return NotFound("Product Not Found");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Product deleted");
        }
    }
}
