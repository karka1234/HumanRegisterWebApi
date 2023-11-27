using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using HumanRegisterWebApi.Services.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.Formats.Jpeg;
using System.Reflection;

namespace HumanRegisterWebApi.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;
        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
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

        public async Task<bool> IsUserFullyRegistered(string userName)
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






        public Task<bool> UpdateUserInfoFields(GetUpSertUserInfoDto updateModel, UserInfo currentUserInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUserAddressFields(GetUpsertUserAddressDto updateModel, UserInfo currentUserInfo)
        {
            throw new NotImplementedException();
        }



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

    }
}
