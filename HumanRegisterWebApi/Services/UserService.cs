using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using HumanRegisterWebApi.Requests;
using HumanRegisterWebApi.Services.Adapters;
using HumanRegisterWebApi.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Reflection;
using System.Security.Claims;

namespace HumanRegisterWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserDataAdapter _userDataAdapter;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger, IUserDataAdapter userDataAdapter)
        {
            _userRepository = userRepository;
            _logger = logger;
            _userDataAdapter = userDataAdapter;
        }
        public async Task<UserInfo> GetCurentUserInfo(string userName)
        {
            var currentUser = await _userRepository.GetUserByUserName(userName);
            return currentUser.UserInfo;
        }
        public async Task<bool> UpsertUserInfoUserAddress(string userName, UserInfo newUserInfo, UserAddress newUserAddress)
        {
            User currentUser = await _userRepository.GetUserByUserName(userName);
            _userRepository.UpsertUserData(currentUser, newUserInfo, newUserAddress);
            return await _userRepository.UpdateDataBase();
        }
        public async Task<bool> UpsertProfileImage(string userName, IFormFile newImage)
        {
            User currentUser = await _userRepository.GetUserByUserName(userName);
            using var memoryStream = new MemoryStream();
            newImage.CopyTo(memoryStream);
            using (var image = Image.Load(memoryStream.ToArray()))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(200, 200),
                    Mode = ResizeMode.Max
                }));

                using var resultStream = new MemoryStream();
                image.Save(resultStream, new JpegEncoder());

                _userRepository.UpsertProfileImage(currentUser, resultStream.ToArray());
                return await _userRepository.UpdateDataBase();
            }
        }

        public async Task<IActionResult> RegisterUserInfoAndUserAddress(string currentUserName, GetUpsertNoImageDTO requestUserData)
        {
            bool registered = await IsUserFullyRegistered(currentUserName);
            if (!registered)
                return LogWarningBadRequest("Already fully registered, you only can change fields one by one");
            User tempUser = _userDataAdapter.Bind(requestUserData);
            bool success = await UpsertUserInfoUserAddress(currentUserName, tempUser.UserInfo, tempUser.UserInfo.UserAddress);
            if (success)//cia galim iskelt
                return LogInformationOk("New user data was added", requestUserData);
            return LogWarningBadRequest("Database not updated");
        }
        public async Task<IActionResult> RegisterOrUpdateProfileImage(string currentUserName, ImageUploadRequest requestImage)
        {
            bool success = await UpsertProfileImage(currentUserName, requestImage.Image);
            if (success)
                return LogInformationOk("New user data was added", requestImage);
            return LogWarningBadRequest("Database not updated");
        }
        public async Task<IActionResult> GetUserInformationDto(string currentUserName)//isorinis
        {
            UserInfo currentUserInfo = await GetCurentUserInfo(currentUserName);
            GetUpsertDTO userDto = _userDataAdapter.Bind(currentUserInfo, currentUserInfo.UserAddress, currentUserInfo.ProfileImage);
            IActionResult nullCheckResult = CheckForNullValuesDTO(currentUserInfo, userDto);
            return nullCheckResult;
        }

        public async Task<(UserInfo, IActionResult)> GetUserInformation(string currentUserName)//vidinis
        {
            UserInfo currentUserInfo = await GetCurentUserInfo(currentUserName);
            IActionResult nullCheckResult = CheckForNullValues(currentUserInfo);
            return (currentUserInfo, nullCheckResult);
        }
        public async Task<bool> ChangeFieldSeperatlyUserInfo(UserInfo currentUserInfo, string propertyName, object request)
        {
            typeof(UserInfo).GetProperty(propertyName).SetValue(currentUserInfo, request);
            return await _userRepository.UpdateDataBase();
        }
        public async Task<bool> ChangeFieldSeperatlyUserAddress(UserAddress currentUserAdress, string propertyName, object request)
        {
            typeof(UserAddress).GetProperty(propertyName).SetValue(currentUserAdress, request);
            return await _userRepository.UpdateDataBase();
        }
        public async Task<IActionResult> UpdateField<T>(string fieldToUpdate, string currentUserName, T request)
        {
            if (IsInvalidRequest(request))
                return LogWarningBadRequest("Value is empty or invalid");

            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            
            PropertyInfo userInfoProperty = typeof(UserInfo).GetProperty(fieldToUpdate);
            PropertyInfo userAddressProperty = typeof(UserAddress).GetProperty(fieldToUpdate);

            bool success = false;
            if (userInfoProperty != null)
                success = await ChangeFieldSeperatlyUserInfo(currentUserInfo, fieldToUpdate, request);
            else if (userAddressProperty != null)
                success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);

            if (success)
                return LogInformationOk("Values was changed", request);

            return LogWarningBadRequest("Not changed");
        }

        private bool IsInvalidRequest<T>(T request)
        {
            if (request is string str && str.IsNullOrEmpty())
                return true;

            if (request is int intValue && (intValue == 0 || intValue < 0))
                return true;

            if (request is long longValue && (longValue == 0 || longValue < 0))
                return true;

            return false;
        }

        private IActionResult LogWarningNotFound(string text)
        {
            _logger.LogWarning(text);
            return new NotFoundObjectResult(text);
        }
        private IActionResult LogInformationOk(string text, object output)
        {
            _logger.LogInformation(text);
            return new OkObjectResult(output);
        }
        private IActionResult LogWarningBadRequest(string text)
        {
            _logger.LogWarning(text);
            return new BadRequestObjectResult(text);
        }
        private IActionResult CheckForNullValuesDTO(UserInfo currentUserInfo, GetUpsertDTO userDto)//nes nori ugrazinti kokiu lauku truksta jei kazkur erroras
        {
            if (currentUserInfo == null)
                return LogWarningNotFound("UserInfo not exist");
            if (currentUserInfo.UserAddress == null)
                return LogWarningNotFound("UserAddress not exist");
            if (currentUserInfo.ProfileImage == null)
                return LogWarningNotFound("ProfileImage not exist");
            return LogInformationOk("Output user info DTO", userDto);
        }


        private IActionResult CheckForNullValues(UserInfo currentUserInfo)//nes nori ugrazinti kokiu lauku truksta jei kazkur erroras
        {
            if (currentUserInfo == null)
                return LogWarningNotFound("UserInfo not exist");
            if (currentUserInfo.UserAddress == null)
                return LogWarningNotFound("UserAddress not exist");
            if (currentUserInfo.ProfileImage == null)
                return LogWarningNotFound("ProfileImage not exist");
            return LogInformationOk("UserInfo", currentUserInfo);
        }

        private async Task<bool> IsUserFullyRegistered(string userName)
        {
            User currentUser = await _userRepository.GetUserByUserName(userName);
            if (currentUser.UserInfo == null)
                return false;
            if (currentUser.UserInfo.UserAddress == null)
                return false;
            if (currentUser.UserInfo.ProfileImage == null)
                return false;
            return true;
        }
    }
}



//ntoes 




/*
 * 
 * 
        public async Task<IActionResult> UpdateFieldString(string fieldToUpdate, string currentUserName, string request)
        {
            if (request.IsNullOrEmpty())
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = false;
            if(typeof(UserInfo).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserInfo(currentUserInfo, fieldToUpdate, request);
            else if(typeof(UserAddress).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }

        public async Task<IActionResult> UpdateFieldLong(string fieldToUpdate, string currentUserName, long request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = false;
            if (typeof(UserInfo).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserInfo(currentUserInfo, fieldToUpdate, request);
            else if (typeof(UserAddress).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
        public async Task<IActionResult> UpdateFieldInt(string fieldToUpdate, string currentUserName, int request)
        {
            if (request == 0 && request < 0)
                return LogWarningBadRequest("Value is empty");
            (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
            if (result.GetType() != typeof(OkObjectResult))
                return result;
            bool success = false;
            if (typeof(UserInfo).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserInfo(currentUserInfo, fieldToUpdate, request);
            else if (typeof(UserAddress).GetProperty(fieldToUpdate) != null)
                success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);
            if (success)
                return LogInformationOk("Values was changed", request);
            return LogWarningBadRequest("Not changedl");
        }
public async Task<IActionResult> UpdateFieldString(string fieldToUpdate, string currentUserName, string request)
{
    if (request.IsNullOrEmpty())
        return LogWarningBadRequest("Value is empty");
    (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
    if (result != null)
        return result;
    bool success = await ChangeFieldSeperatlyUserInfo(currentUserInfo, fieldToUpdate, request);
    if (success)
        return LogInformationOk("Values was changed", request);
    return LogWarningBadRequest("Not changedl");
}



public async Task<IActionResult> UpdateFieldLong(string fieldToUpdate, string currentUserName, long request)
{
    if (request == 0 && request < 0)
        return LogWarningBadRequest("Value is empty");
    (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
    if (result != null)
        return result;
    bool success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);
    if (success)
        return LogInformationOk("Values was changed", request);
    return LogWarningBadRequest("Not changedl");
}
public async Task<IActionResult> UpdateFieldInt(string fieldToUpdate, string currentUserName, long request)
{
    if (request == 0 && request < 0)
        return LogWarningBadRequest("Value is empty");
    (UserInfo currentUserInfo, IActionResult result) = await GetUserInformation(currentUserName);
    if (result != null)
        return result;
    bool success = await ChangeFieldSeperatlyUserAddress(currentUserInfo.UserAddress, fieldToUpdate, request);
    if (success)
        return LogInformationOk("Values was changed", request);
    return LogWarningBadRequest("Not changedl");
}

*/


/*
public async Task<bool> UpdateUserInfoFields(GetUpSertUserInfoDto updateModel, UserInfo currentUserInfo)
{
    if(!CheckChangedUserInfoFields(updateModel, currentUserInfo))
        return false;
    return await _userRepository.UpdateDataBase();
}



public async Task<bool> UpdateUserAddressFields(GetUpsertUserAddressDto updateModel, UserInfo currentUserInfo)
{
    if (!CheckUserAddressFields(updateModel, currentUserInfo.UserAddress))
        return false;
    return await _userRepository.UpdateDataBase();
}

private bool CheckChangedUserInfoFields(GetUpSertUserInfoDto updateModel, UserInfo currentUserInfo)
{
    bool somethingUpdated = false;
    var properties = typeof(GetUpSertUserInfoDto).GetProperties();
    foreach (var property in properties)
    {
        var updatedValue = property.GetValue(updateModel);
        if (IsPropertyValueValid(updatedValue))
        {
            typeof(UserInfo).GetProperty(property.Name)?.SetValue(currentUserInfo, updatedValue);
            somethingUpdated = true;
        }
        else
            _logger.LogWarning($"Invalid value {updatedValue} for property {property.Name}");
    }
    return somethingUpdated;
}


private bool CheckUserAddressFields(GetUpsertUserAddressDto updateModel, UserAddress currentUserAddress)
{
    bool somethingUpdated = false;
    var properties = typeof(GetUpsertUserAddressDto).GetProperties();
    foreach (var property in properties)
    {
        var updatedValue = property.GetValue(updateModel);
        if (IsPropertyValueValid(updatedValue))
        {
            typeof(UserAddress).GetProperty(property.Name)?.SetValue(currentUserAddress, updatedValue);
            somethingUpdated = true;
        }
        else
            _logger.LogWarning($"Invalid value {updatedValue} for property {property.Name}");
    }
    return somethingUpdated;
}


private bool IsPropertyValueValid(object value)
{
    if (value is string stringValue)
        return !string.Equals(stringValue, "string") && !string.IsNullOrEmpty(stringValue);
    if (value is int intValue)
        return !(intValue == 0) && !(intValue < 0);
    if (value is long longValue)
        return !(longValue == 0) && !(longValue < 0);
    return true;
}
*/