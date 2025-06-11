using CarbonPulse.API.DTOs;
using CarbonPulse.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarbonPulse.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Register a new user and return a JWT token.
        /// </summary>
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserRegisterDto request)
        {
            if (await _authService.UserExists(request.Username))
                return BadRequest("Username already exists");

            var response = await _authService.Register(request);
            return Ok(response);
        }

        /// <summary>
        /// Login with username and password to receive a JWT.
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(UserLoginDto request)
        {
            try
            {
                var response = await _authService.Login(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials");
            }
        }
    }
}
