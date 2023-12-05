using HumanRegisterWebApi.Services;
using HumanRegisterWebApi.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = (nameof(Role.Admin)))]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IAdminService adminService, IUserRepository userRepository, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _userRepository = userRepository;
            _logger = logger;
        }
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> GetCurentUserInfo([FromRoute] Guid id)
        {
            bool idExists = _adminService.CheckUserIdExists(id).Result;
            if (!idExists)
                return LogWarningNotFound($"{id} not found");

            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            bool yourAccount = _adminService.CheckCurrentUserWithGivenUserId(currentUserName, id).Result;
            if (yourAccount)
                return LogWarningBadRequest("You cannot delete your account");

            bool success = await _adminService.DeleteUserInfo(id);
            if (!success)
                LogWarningBadRequest("Delete was not succesfull");
            return LogInformationOk($"{id} was deleted", id);
        }

        private IActionResult LogWarningNotFound(string text)
        {
            _logger.LogWarning(text);
            return NotFound(text);
        }
        private IActionResult LogInformationOk(string text, object output)
        {
            _logger.LogInformation(text);
            return Ok(output);
        }
        private IActionResult LogWarningBadRequest(string text)
        {
            _logger.LogWarning(text);
            return BadRequest(text);
        }
    }
}
