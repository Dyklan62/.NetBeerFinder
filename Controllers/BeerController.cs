using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly BeerContext _context;

        public BeerController(BeerContext context)
        {
            _context = context;
        }

        // GET: api/Beer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeer()
        {
            return await _context.Beer.ToListAsync();
        }

        // GET: api/Beer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(long id)
        {
            var beer = await _context.Beer.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        // PUT: api/Beer
        [HttpPut]
        public async Task<IActionResult> PutBeer([FromBody] Beer beer)
        {
            if (!BeerExists(beer.Name))
            {
                _context.Entry(beer).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return NoContent();
        }

        // POST: api/Beer
        [HttpPost]
        public async Task<ActionResult<Beer>> PostUtilisateur(Beer beer)
        {
            _context.Beer.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBeer", new { id = beer.Id }, beer);
        }

        // DELETE: api/Beer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(long id)
        {
            var beer = await _context.Beer.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beer.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(string name)
        {
            return _context.Beer.Any(e => e.Name == name);
        }
    }
}
