using Backend.Context;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;
        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<Backend.Entities.User>> LoginUser([FromBody] Backend.Entities.DTO.UserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user is null)
            {
                return NotFound("Wrong Login");
            }
            if (!Backend.Help.PasswordHasher.VerifyPassword(user.Login, dto.Password, user.Password))
            {
                return BadRequest("Wrong password");
            }
            return Ok(user.Id);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] Backend.Entities.DTO.UserDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == dto.Login);
            if (user is null)
            {
                var User = new Backend.Entities.User();
                User.Login = dto.Login;
                User.Password = Backend.Help.PasswordHasher.CreateHashPassword(dto.Login, dto.Password);
                await _context.Users.AddAsync(User);
                await _context.SaveChangesAsync();
                return Ok("User created");
            }
            return NotFound("Login is already taken!");
        }

        
    }
}
