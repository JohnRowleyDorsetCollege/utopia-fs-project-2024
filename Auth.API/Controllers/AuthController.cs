using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Replace with actual user validation logic
            if (request.Username == "xxxx" && request.Password == "yyyy")
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { Token = token });
            }

            return Unauthorized();
        }
    }
}
