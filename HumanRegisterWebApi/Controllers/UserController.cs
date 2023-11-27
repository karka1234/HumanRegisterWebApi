using Azure.Core;
using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using HumanRegisterWebApi.Requests;
using HumanRegisterWebApi.Services;
using HumanRegisterWebApi.Services.Adapters;
using HumanRegisterWebApi.Services.Data;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ILogger<UserController> _logger;
        private readonly IUserInfoAdapter _userInfoAdapter;
        private readonly IUserAddressAdapter _userAddressAdapter;
        private readonly IUserDataAdapter _userDataAdapter;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService service, ILogger<UserController> logger, IUserInfoAdapter userInfoAdapter, IUserAddressAdapter userAddressAdapter, IUserDataAdapter userDataAdapter, IUserRepository userRepository)
        {
            _service = service;
            _logger = logger;
            _userInfoAdapter = userInfoAdapter;
            _userAddressAdapter = userAddressAdapter;
            _userDataAdapter = userDataAdapter;
            _userRepository = userRepository;
        }

        [HttpGet("GetCurrentUserInfo")]
        public async Task<IActionResult> GetCurentUserInfo()
        {
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();//tuple nayyys
            if (result != null)
                return result;
            GetUpsertDTO temp = _userDataAdapter.Bind(currentUserInfo, currentUserInfo.UserAddress, currentUserInfo.ProfileImage);
            return LogInformationOk(string.Empty, temp);
        }
        [HttpPost("RegisterUserInfoAndUserAddress")]
        public async Task<IActionResult> AddOrUpdateUserInfo([FromBody] GetUpsertNoImageDTO requestUserData)
        {
            string currentUserName = GetCurrentUser();
            bool registered = await _service.IsUserFullyRegistered(currentUserName);
            if (registered)
                return LogWarningBadRequest("Already fully registered");
            User tempUser = _userDataAdapter.Bind(requestUserData);//laukau temp objekte visas lenteles apie vartotoja nezinau ar taip gerai
            bool success = await _service.UpsertUserInfoUserAddress(currentUserName, tempUser.UserInfo, tempUser.UserInfo.UserAddress);
            if (success)
                return LogInformationOk(string.Empty, requestUserData);
            return LogWarningBadRequest("Database not updated");
        }
        [HttpPost("RegisterOrUpdateProfileImage")]
        public async Task<IActionResult> AddOrUpdateProfilePicture([FromForm] ImageUploadRequest requestImage)
        {
            string currentUserName = GetCurrentUser();
            bool success = await _service.UpsertProfileImage(currentUserName, requestImage.Image);
            if (success)
                return LogInformationOk(string.Empty, requestImage);
            return LogWarningBadRequest("Database not updated");
        }

        [HttpPut("UpdateFirstName/{request}")]
        public async Task<IActionResult> UpdateFirstName([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
              return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
              return result;

            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "FirstName", request);

            if (success)
              return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateSecondName/{request}")]
        public async Task<IActionResult> UpdateSecondName([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "SecName", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdatePersonCode/{request}")]
        public async Task<IActionResult> UpdatePersonCode([FromRoute] long request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "PersonCode", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateEmail/{request}")]
        public async Task<IActionResult> UpdateEmail([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "Email", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdatePhoneNumber/{request}")]
        public async Task<IActionResult> UpdatePhoneNumber([FromRoute] long request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserInfo(currentUserInfo, "PhoneNumber", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }

        [HttpPut("UpdateCity/{request}")]
        public async Task<IActionResult> UpdateCity([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "City", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateStreet/{request}")]
        public async Task<IActionResult> UpdateStreet([FromRoute] string request)
        {
            if (request.IsNullOrEmpty())
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "Street", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateHouseNumber/{request}")]
        public async Task<IActionResult> UpdateHouseNumber([FromRoute] int request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "HouseNmber", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        [HttpPut("UpdateApartamentNumper/{request}")]
        public async Task<IActionResult> UpdateApartamentNumper([FromRoute] int request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation();
            if (result != null)
                return result;
            bool success = await _service.ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, "ApartamentNumber", request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }





        private async Task<(UserInfo, IActionResult)> GetUserInformation()
        {
            string currentUserName = GetCurrentUser();
            UserInfo currentUserInfo = await _service.GetCurentUserInfo(currentUserName);
            IActionResult nullCheckResult = CheckForNullValues(currentUserInfo);
            return (currentUserInfo, nullCheckResult);
        }
        private IActionResult CheckForNullValues(UserInfo currentUserInfo)//nes nori ugrazinti kokiu lauku truksta jei kazkur erroras
        {
            if (currentUserInfo == null)
                return LogWarningNotFound("UserInfo not exist");
            if (currentUserInfo.UserAddress == null)
                return LogWarningNotFound("UserAddress not exist");
            if (currentUserInfo.ProfileImage == null)
                return LogWarningNotFound("ProfileImage not exist");
            return null;
        }
        private string GetCurrentUser()
        {
            return User.FindFirst(ClaimTypes.Name)?.Value;
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
    }
}
