using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using System.Reflection;
using System.Threading.Tasks;

namespace HumanRegisterWebApi.Services
{
    public interface IUserService
    {
        Task<bool> ChangeFieldSeperatlyUserInfo(UserInfo currentUserInfo, string propertyName, object request);
        Task<bool> ChangeFieldSeperatlyUserAddress(UserAddress currentUserAdress, string propertyName, object request);
        //Task<bool> ChangeFirstName(UserInfo currentUserInfo, string request);
        Task<UserInfo> GetCurentUserInfo(string userName);
        Task<bool> UpsertProfileImage(string userName, IFormFile newImage);
        Task<bool> UpsertUserInfoUserAddress(string userName, UserInfo newUserInfo, UserAddress newUserAddress);
        Task<bool> UpdateUserInfoFields(GetUpSertUserInfoDto updateModel, UserInfo currentUserInfo);
        Task<bool> UpdateUserAddressFields(GetUpsertUserAddressDto updateModel, UserInfo currentUserInfo);
        Task<bool> IsUserFullyRegistered(string userName);
    }
}