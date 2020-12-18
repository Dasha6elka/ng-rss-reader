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
    public class ChannelsController : ControllerBase
    {
        private readonly RssReaderContext _context;

        public ChannelsController(RssReaderContext context)
        {
            _context = context;
        }

        // GET: api/Channels
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetChannels()
        {
            return await _context.Channels
                .Where(x => x.IdUser == int.Parse(User.Identity.Name))
                .Select(x => new { Id = x.IdChannel, Channel = x.Name, Link = x.Link })
                .ToListAsync();
        }

        // GET: api/Channels/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> GetChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);

            if (channel == null)
            {
                return NotFound();
            }

            return channel;
        }

        // PUT: api/Channels/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChannel(int id, Channel channel)
        {
            if (id != channel.IdChannel)
            {
                return BadRequest();
            }

            _context.Entry(channel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChannelExists(id))
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

        // POST: api/Channels
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<object>> PostChannel(DTO.ChannelDTO dto)
        {
            var channel = new Channel
            {
                Name = dto.Channel,
                Link = dto.Link,
                IdUser = int.Parse(User.Identity.Name),
            };

            _context.Channels.Add(channel);
            await _context.SaveChangesAsync();

            return new { Id = channel.IdChannel, Channel = channel.Name, Link = channel.Link };
        }

        // DELETE: api/Channels/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Channel>> DeleteChannel(int id)
        {
            var channel = await _context.Channels.FindAsync(id);
            if (channel == null)
            {
                return NotFound();
            }

            _context.Channels.Remove(channel);
            await _context.SaveChangesAsync();

            return channel;
        }

        private bool ChannelExists(int id)
        {
            return _context.Channels.Any(e => e.IdChannel == id);
        }
    }
}
