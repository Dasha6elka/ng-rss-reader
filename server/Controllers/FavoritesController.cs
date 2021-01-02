using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly RssReaderContext _context;

        public FavoritesController(RssReaderContext context)
        {
            _context = context;
        }

        // GET: api/Favorites
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetFavorites()
        {
            return await _context.Favorites
                .Where(x => x.IdUser == int.Parse(User.Identity.Name))
                .Select(x =>new { Id = x.IdFavorite, Link = x.Link, Title = x.Title })
                .ToListAsync();
        }

        // GET: api/Favorites/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Favorite>> GetFavorite(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            return favorite;
        }

        // PUT: api/Favorites/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFavorite(int id, DTO.FavoriteDTO dto)
        {
            var favorite = await _context.Favorites.FindAsync(id);

            if (favorite == null)
            {
                return NotFound();
            }

            if (id != favorite.IdFavorite)
            {
                return BadRequest();
            }

            favorite.Link = dto.Link;
            favorite.Title = dto.Title;

            _context.Entry(favorite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FavoriteExists(id))
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

        // POST: api/Favorites
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<object>> PostFavorite(DTO.FavoriteDTO dto)
        {
            var favorite = new Favorite
            {
                Link = dto.Link,
                Title = dto.Title,
                IdUser = int.Parse(User.Identity.Name)
            };
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();

            return new { Id = favorite.IdFavorite, Link = favorite.Link, Title = favorite.Title };
        }

        // DELETE: api/Favorites/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Favorite>> DeleteFavorite(int id)
        {
            var favorite = await _context.Favorites.FindAsync(id);
            if (favorite == null)
            {
                return NotFound();
            }

            _context.Favorites.Remove(favorite);
            await _context.SaveChangesAsync();

            return favorite;
        }

        private bool FavoriteExists(int id)
        {
            return _context.Favorites.Any(e => e.IdFavorite == id);
        }
    }
}
