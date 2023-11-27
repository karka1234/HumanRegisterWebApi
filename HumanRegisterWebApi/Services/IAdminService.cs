using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services
{
    public interface IAdminService
    {
        Task<bool> DeleteUserInfo(Guid id);
        Task<bool> CheckUserIdExists(Guid id);
        Task<bool> CheckCurrentUserWithGivenUserId(string currentUserName, Guid givenId);
    }
}