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


        [HttpPost("RegisterOrUpdateProfileImage")]
        public async Task<IActionResult> RegisterOrUpdateProfileImage([FromForm] ImageUploadRequest requestImage)
        {
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            IActionResult result = await _service.RegisterOrUpdateProfileImage(currentUserName, requestImage);
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



//notes 

/*
        public async Task<(UserInfo, IActionResult)> GetUserInformation()
        {
            string currentUserName = GetCurrentUser();
            UserInfo currentUserInfo = await _service.GetCurentUserInfo(currentUserName);
            IActionResult nullCheckResult = CheckForNullValues(currentUserInfo);
            return (currentUserInfo, nullCheckResult);
        }
        public IActionResult CheckForNullValues(UserInfo currentUserInfo)//nes nori ugrazinti kokiu lauku truksta jei kazkur erroras
        {
            if (currentUserInfo == null)
                return LogWarningNotFound("UserInfo not exist");
            if (currentUserInfo.UserAddress == null)
                return LogWarningNotFound("UserAddress not exist");
            if (currentUserInfo.ProfileImage == null)
                return LogWarningNotFound("ProfileImage not exist");
            return null;
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
        */
/*     [HttpPut("UpdateUserFields")]
public async Task<IActionResult> UpdateUserFields([FromBody] GetUpSertUserInfoDto updateModel)//cia galima atnaujinti
{
 (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
 bool success = await _service.UpdateUserInfoFields(updateModel, currentUserInfo);
 if (success)
     return LogInformationOk("Values was changed", updateModel);            
 return LogWarningBadRequest("Nothing was changed");
}
[HttpPut("UpdateUserAddressFields")]
public async Task<IActionResult> UpdateUserAddressFields([FromBody] GetUpsertUserAddressDto updateModel)//cia galima atnaujinti
{
 (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
 bool success = await _service.UpdateUserAddressFields(updateModel, currentUserInfo);
 if (success)
     return LogInformationOk("Values was changed", updateModel);
 return LogWarningBadRequest("Nothing was changed");
}*/

/*
[HttpPut("UpdateFirstName/{request}")]
public async Task<IActionResult> UpdateFirstName([FromRoute] string request)
{            
    if (request.IsNullOrEmpty())
        return _service.LogWarningBadRequest("Value is empty");
    string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
    ///cia labai stipriai galim perkelti viska i serviza...
    (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
    if (result.GetType() == typeof(OkObjectResult))
    {
        bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "FirstName", request);
        if (success)
            result = _service.LogInformationOk("Values was changed", request);                
    }
    else
        result = _service.LogWarningBadRequest("Not changed");
    return result;



        [HttpPut("UpdateSecondName/{request}")]
        public async Task<IActionResult> UpdateSecondName([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "SecName", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdatePersonCode/{request}")]
        public async Task<IActionResult> UpdatePersonCode([FromRoute] long request)
        {
            if (request == 0 && request < 0)
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "PersonCode", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateEmail/{request}")]
        public async Task<IActionResult> UpdateEmail([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "Email", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdatePhoneNumber/{request}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] long request)
        {
            if (request == 0 && request < 0)
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "PhoneNumber", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateCity/{request}")]
        public async Task<IActionResult> UpdateCity([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "City", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateStreet/{request}")]
        public async Task<IActionResult> UpdateStreet([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "Street", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateHouseNumber/{request}")]
        public async Task<IActionResult> UpdateHouseNumber([FromRoute] int request)
        {
            if (request == 0 && request < 0)
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "HouseNmber", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateApartamentNumper/{request}")]
        public async Task<IActionResult> UpdateApartamentNumper([FromRoute] int request)
        {
            if (request == 0 && request < 0)
                return _service.LogWarningBadRequest("Value is empty");
            string currentUserName = User.FindFirst(ClaimTypes.Name)?.Value;
            (UserInfo currentUserInfo, IActionResult result) = await _service.GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "ApartamentNumber", request);
            if (success)
                return _service.LogInformationOk("Values was changed", request);
            return _service.LogWarningBadRequest("Not changedl");
        }


}*/