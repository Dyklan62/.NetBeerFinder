using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Api_dotnet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BeerController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Beer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeer()
        {
            return await _context.Beers.ToListAsync();
        }

        // GET: api/Beer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(long id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return beer;
        }

        // GET: api/Beer/Type
        [HttpGet("search/{Type}")]
        public async Task<IActionResult> GetBeerByType(string type)
        {
            var beers = await _context.Beers.FirstAsync(e => e.Type == type); ;
            if (beers == null)
            {
                return NotFound();
            }

            return Ok(beers);
        }

        // GET: api/Beer/Name
        [HttpGet("search/{Name}")]
        public async Task<IActionResult> GetBeerByName(string name)
        {
            var beers = await _context.Beers.FirstAsync(e => e.Name == name); ;
            if (beers == null)
            {
                return NotFound();
            }

            return Ok(beers);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // PUT: api/Beer
        [HttpPut]
        public async Task<IActionResult> PutBeer([FromBody] Beer beer)
        {
            if (!BeerExists(beer.Id))
            {
                return NotFound();
            }

            if (IsBeerUpdate(beer))
            {
                return BadRequest("Please update Beer information");
            }

            try
            {
                _context.Entry(beer).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return Ok("Beer update");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // POST: api/Beer
        [HttpPost]
        public async Task<ActionResult<Beer>> PostUtilisateur(Beer beer)
        {
            _context.Beers.Add(beer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return CreatedAtAction("GetBeer", new { id = beer.Id }, beer);
        }

        // DELETE: api/Beer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(long id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(long Id)
        {
            return _context.Beers.Any(e => e.Id == Id);
        }

        private bool IsBeerUpdate(Beer beer)
        {

            var predicate = _context.Beers.Any(e => e.Id == beer.Id && e.Name == beer.Name && e.Tagline == beer.Tagline
            && e.Type == beer.Type && e.Url == beer.Url && e.Description == beer.Description && e.Date == beer.Date && e.Abv == beer.Abv);
            Console.WriteLine(predicate);
            return predicate;
        }
    }
}
