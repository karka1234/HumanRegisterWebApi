using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Data
{
    public interface IUserRepository
    {
        Task DeleteUserInfo(User userToDelete);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByUserName(string userName);
        Task<bool> UpdateDataBase();
        void UpsertUserData(User currentUser, UserInfo newUserInfo, UserAddress newUserAddress);
        void UpsertProfileImage(User currentUser, byte[] newImageBytes);
    }
}