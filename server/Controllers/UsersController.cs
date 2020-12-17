using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RssReaderContext _context;

        public UsersController(RssReaderContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.IdUser)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users/register
        [HttpPost("register")]
        public async Task<ActionResult<object>> PostUser(DTO.UserDTO dto)
        {
            var user = new User {
                Login = dto.Username,
                Password = dto.Password
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await GenerateToken(dto);
        }

        // DELETE: api/Users/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.IdUser == id);
        }

        // POST: api/Users/token
        [HttpPost("token")]
        public async Task<ActionResult<object>> Token(DTO.UserDTO dto)
        {
            return await GenerateToken(dto);
        }

        private async Task<ActionResult<object>> GenerateToken(DTO.UserDTO dto)
        {
            var (identity, person) = await GetIdentity(dto.Username, dto.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password" });
            }

            var now = DateTime.UtcNow;

            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = token,
                user_id = identity.Name,
            };

            return response;
        }

        private async Task<ValueTuple<ClaimsIdentity, User>> GetIdentity(string username, string password)
        {
            var person = await _context.Users.Where(x => x.Login == username && x.Password == password).FirstOrDefaultAsync();

            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.IdUser.ToString())
                };

                return (new ClaimsIdentity(claims, "Token"), person);
            }

            // если пользователя не найдено
            return (null, person);
        }
    }
}
