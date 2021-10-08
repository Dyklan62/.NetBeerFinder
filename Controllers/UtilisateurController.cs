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
    public class UtilisateurController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UtilisateurController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilisateur
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilisateur>>> GetUtilisateur()
        {
            return await _context.Utilisateur.ToListAsync();
        }

        // GET: api/Utilisateur/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateur(long id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);

            if (utilisateur == null)
            {
                return NotFound();
            }

            return utilisateur;
        }

        // PUT: api/Utilisateur
        [HttpPut]
        public async Task<IActionResult> PutUtilisateur([FromBody] Utilisateur utilisateur)
        {
            if (!UtilisateurExists(utilisateur.Id))
            {
                return NotFound();
            }

            if (IsUtilisateurUpdate(utilisateur))
            {
                return BadRequest("Please update Utilisateur information");
            }

            try
            {
                _context.Entry(utilisateur).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return Ok("Utilisateur update");
        }

        // POST: api/Utilisateur
        [HttpPost]
        public async Task<ActionResult<Utilisateur>> Register(Utilisateur utilisateur)
        {
            if (UtilisateurExists(utilisateur.Id))
            {
                return Conflict("utilisateur already exist");
            }
            utilisateur.MotDePasse = BCrypt.Net.BCrypt.HashPassword(utilisateur.MotDePasse);

            _context.Utilisateur.Add(utilisateur);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return CreatedAtAction("GetUtilisateur", new { id = utilisateur.Id }, utilisateur);
        }

        // DELETE: api/Utilisateur/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilisateur(long id)
        {
            var utilisateur = await _context.Utilisateur.FindAsync(id);
            if (utilisateur == null)
            {
                return NotFound();
            }

            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilisateurExists(long Id)
        {
            return _context.Utilisateur.Any(e => e.Id == Id);
        }

        private bool IsUtilisateurUpdate(Utilisateur utilisateur)
        {
            return _context.Utilisateur.Any(e => e == utilisateur);
        }

    }
}
