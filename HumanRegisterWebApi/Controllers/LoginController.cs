using HumanRegisterWebApi.Requests;
using HumanRegisterWebApi.Services;
using Microsoft.AspNetCore.Mvc;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Controllers
{
    [Route("Login/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IJwtService _jwtService;

        public LoginController(IJwtService jwtService, ILoginService loginService)
        {
            _jwtService = jwtService;
            _loginService = loginService;
        }

        [HttpPost("RegisterNewUser")]
        public IActionResult RegisterNewUser([FromBody] SignupNewAccountRequest request)
        {
            var account = _loginService.RegisterNewUser(request.UserName, request.Password);
            if(account == null) 
                return BadRequest("User already exists");
            return Ok(account);
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var response = _loginService.Login(request.UserName, request.Password);
            if (!response.isSuccess)
                return Unauthorized();
            return Ok(_jwtService.GetJwtToken(request.UserName, (Role)response.Role));//cia gal ID naudoti
        }

    }
}
