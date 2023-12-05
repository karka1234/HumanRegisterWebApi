using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using HumanRegisterWebApi.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HumanRegisterWebApi.Services
{
    public interface IUserService
    {
        Task<IActionResult> RegisterOrUpdateProfileImage(string currentUserName, ImageUploadRequest requestImage);
        Task<IActionResult> RegisterUserInfoAndUserAddress(string currentUserName, GetUpsertNoImageDTO requestUserData);
        Task<bool> ChangeFieldSeperatlyUserAddress(UserAddress currentUserAdress, string propertyName, object request);
        Task<bool> ChangeFieldSeperatlyUserInfo(UserInfo currentUserInfo, string propertyName, object request);
        Task<UserInfo> GetCurentUserInfo(string userName);
        Task<(UserInfo, IActionResult)> GetUserInformation(string currentUserName);
        Task<IActionResult> GetUserInformationDto(string currentUserName);
        Task<IActionResult> UpdateField<T>(string fieldToUpdate, string currentUserName, T request);
        Task<bool> UpsertProfileImage(string userName, IFormFile newImage);
        Task<bool> UpsertUserInfoUserAddress(string userName, UserInfo newUserInfo, UserAddress newUserAddress);
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