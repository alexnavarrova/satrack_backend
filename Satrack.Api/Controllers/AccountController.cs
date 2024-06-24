using Microsoft.AspNetCore.Mvc;
using Satrack.Application.Models.Identity;
using Satrack.Application.Contracts.Identity;

namespace Satrack.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponse>> Login([FromBody] AuthRequest request)
        {
            return Ok(await _authService.Login(request));
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthResponse>> Register([FromBody] RegistrationRequest request)
        {
            return Ok(await _authService.Register(request));
        }

    }
}

