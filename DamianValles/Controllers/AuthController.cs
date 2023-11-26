using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WeightLifting.Library.Business.Interfaces;
using WeightLifting.Library.Logger;
using WeightLifting.Models.DTOs;
using WeightLiftingLibrary.Business.Interfaces;

namespace WeightLifting.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authcontroller : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILoggerService _logger;

        public Authcontroller(IAuthService authService, ILoggerService logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO model)
        {
            if(_authService.IsValidUser(model.Username, model.Password))
            {
                return Ok(_authService.CreateToken(model.Username));
            }

            return Unauthorized("Invalid user or password");
        }
    }
}
