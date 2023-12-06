using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Requests;
using HumanRegisterWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = (nameof(Role.User)))]
    [Route("User/[controller]")]
    [ApiController]
    public class UserController : ControllerBase////isvedimus visus i serviza perkelt kad kuo maziau logikos cia butu. O is servizo turbut i dar kokia validaciju klase
                                                //pasidomet apie dispose. garbage collector............................
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetUserInformationDto")]
        public async Task<IActionResult> GetUserInformationDto()
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            IActionResult result = await _service.GetUserInformationDto(currentUserName);
            return result;
        }
        [HttpPost("RegisterUserInfoAndUserAddress")]
        public async Task<IActionResult> RegisterUserInfoAndUserAddress([FromBody] GetUpsertNoImageDTO requestUserData)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            IActionResult result = await _service.RegisterUserInfoAndUserAddress(currentUserName, requestUserData);
            return result;
        }
        [HttpPost("RegisterProfileImage")]
        public async Task<IActionResult> RegisterProfileImage([FromForm] ImageUploadRequest requestImage)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            IActionResult result = await _service.RegisterProfileImage(currentUserName, requestImage);
            return result;
        }
        [HttpPut("UpdateFirstName/{request}")]
        public async Task<IActionResult> UpdateFirstName([FromRoute] string request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<string>("FirstName", currentUserName, request);
        }

        [HttpPut("UpdateSecondName/{request}")]
        public async Task<IActionResult> UpdateSecondName([FromRoute] string request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<string>("SecName", currentUserName, request);

        }
        [HttpPut("UpdatePersonCode/{request}")]
        public async Task<IActionResult> UpdatePersonCode([FromRoute] long request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<long>("PersonCode", currentUserName, request);

        }
        [HttpPut("UpdateEmail/{request}")]
        public async Task<IActionResult> UpdateEmail([FromRoute] string request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<string>("Email", currentUserName, request);

        }
        [HttpPut("UpdatePhoneNumber/{request}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] long request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<long>("PhoneNumber", currentUserName, request);
        }
        [HttpPut("UpdateCity/{request}")]
        public async Task<IActionResult> UpdateCity([FromRoute] string request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<string>("City", currentUserName, request);
        }
        [HttpPut("UpdateStreet/{request}")]
        public async Task<IActionResult> UpdateStreet([FromRoute] string request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<string>("Street", currentUserName, request);
        }
        [HttpPut("UpdateHouseNumber/{request}")]
        public async Task<IActionResult> UpdateHouseNumber([FromRoute] int request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<int>("HouseNmber", currentUserName, request);
        }
        [HttpPut("UpdateApartamentNumper/{request}")]
        public async Task<IActionResult> UpdateApartamentNumper([FromRoute] int request)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            return await _service.UpdateField<int>("ApartamentNumber", currentUserName, request);


        }
    }
}