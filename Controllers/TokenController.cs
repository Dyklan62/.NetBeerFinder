using System.Linq;
using System;
using System.Threading.Tasks;
using JwtAuthentication.Server.Service;
using JwtAuthentification.server.Interface;
using JwtAuthentification.server.Interface.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

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
        public IActionResult Login([FromBody] Login login)
        {
            IActionResult result = this.Unauthorized();

            if (!String.IsNullOrEmpty(login.Email) && !String.IsNullOrEmpty(login.MotDePasse))
            {
                var utilisateur = _context.Utilisateur.Where(u => u.Email.Equals(login.Email) && u.MotDePasse.Equals(login.MotDePasse)); ;

                utilisateur.First().Token = _tokenService.BuildToken(login.Email);

                result = this.Ok(utilisateur.First().Token);
            }

            return result;
        }



    }
}