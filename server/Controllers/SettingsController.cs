using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using server;
using server.Models;

namespace server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly RssReaderContext _context;

        public SettingsController(RssReaderContext context)
        {
            _context = context;
        }

        // GET: api/Settings
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Settings>>> GetSettings()
        {
            return await _context.Settings.ToListAsync();
        }

        // GET: api/Settings/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Settings>> GetSettings(int id)
        {
            var settings = await _context.Settings.FindAsync(id);

            if (settings == null)
            {
                return NotFound();
            }

            return settings;
        }

        // PUT: api/Settings/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSettings(int id, Settings settings)
        {
            if (id != settings.IdSettings)
            {
                return BadRequest();
            }

            _context.Entry(settings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SettingsExists(id))
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

        // POST: api/Settings
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Settings>> PostSettings(Settings settings)
        {
            _context.Settings.Add(settings);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSettings", new { id = settings.IdSettings }, settings);
        }

        // DELETE: api/Settings/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Settings>> DeleteSettings(int id)
        {
            var settings = await _context.Settings.FindAsync(id);
            if (settings == null)
            {
                return NotFound();
            }

            _context.Settings.Remove(settings);
            await _context.SaveChangesAsync();

            return settings;
        }

        private bool SettingsExists(int id)
        {
            return _context.Settings.Any(e => e.IdSettings == id);
        }
    }
}
