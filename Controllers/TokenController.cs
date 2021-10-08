using System.Linq;
using System;
using System.Threading.Tasks;
using JwtAuthentification.server.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace JwtAuthentication.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;

        private readonly AppDbContext _context;


        public TokenController(IJwtTokenService tokenService, AppDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            IActionResult result = this.Unauthorized();

            if (String.IsNullOrEmpty(login.Email) && String.IsNullOrEmpty(login.MotDePasse))
            {
                return BadRequest("email or password empty");
            }

            var utilisateur = _context.Utilisateur.SingleOrDefault(u => u.Email.Equals(login.Email));


            if (utilisateur == null)
            {
                return BadRequest("bad account");
            }

            if (!BCrypt.Net.BCrypt.Verify(login.MotDePasse, utilisateur.MotDePasse))
            {
                return BadRequest("bad password");
            }

            utilisateur.Token = _tokenService.BuildToken(login.Email);

            try
            {
                _context.Entry(utilisateur).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return this.Ok(utilisateur.Token);

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] Utilisateur utilisateur)
        {
            IActionResult result = this.Unauthorized();

            if (String.IsNullOrEmpty(utilisateur.Email))
            {
                return BadRequest("email empty");
            }

            var user = _context.Utilisateur.SingleOrDefault(u => u.Email.Equals(utilisateur.Email)); ;
            user.Token = "";

            if (utilisateur == null)
            {
                return BadRequest("bad account");
            }

            try
            {
                _context.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return this.Ok();

        }

    }
}