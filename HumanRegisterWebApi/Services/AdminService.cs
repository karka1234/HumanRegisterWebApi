using HumanRegisterWebApi.Models;
using HumanRegisterWebApi.Services.Data;
using Microsoft.EntityFrameworkCore;

namespace HumanRegisterWebApi.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserRepository _userRepository;

        public AdminService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUserInfo(Guid id)
        {
            User user = _userRepository.GetUserById(id).Result;
            _userRepository.DeleteUserInfo(user);
            return await _userRepository.UpdateDataBase();
        }


        public async Task<bool> CheckUserIdExists(Guid id)
        {
            return await _userRepository.GetUserById(id) != null;
        }

        public async Task<bool> CheckCurrentUserWithGivenUserId(string currentUserName, Guid givenId)
        {
            return await _userRepository.GetUserById(givenId) == await _userRepository.GetUserByUserName(currentUserName);
        }
    }
}
