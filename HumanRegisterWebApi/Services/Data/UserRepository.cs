using HumanRegisterWebApi.Database;
using HumanRegisterWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanRegisterWebApi.Services.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> UpdateDataBase()
        {
            int affectedRows = await _context.SaveChangesAsync();
            return affectedRows > 0;
        }
        public void UpsertUserData(User currentUser, UserInfo newUserInfo, UserAddress newUserAddress)
        {
            UpsertUserInfo(currentUser, newUserInfo);
            UpsertUserAddress(currentUser, newUserAddress);
        }
        public void UpsertProfileImage(User currentUser, byte[] newImageBytes)
        {
            if (currentUser.UserInfo.ProfileImage == null)
            {
                currentUser.UserInfo.ProfileImage = new ProfileImage() { ImageBytes = newImageBytes };
            }
            else
            {
                currentUser.UserInfo.ProfileImage.ImageBytes = newImageBytes;
            }
        }

        public async Task<User> GetUserById(Guid id)
        {
            var user = await _context.Users
                .Include(ui => ui.UserInfo)
                    .ThenInclude(ui => ui.UserAddress)
                .Include(ui => ui.UserInfo)
                    .ThenInclude(ui => ui.ProfileImage)
                .FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }
        public async Task<User> GetUserByUserName(string userName)
        {
            var user = await _context.Users
                .Include(ui => ui.UserInfo)
                    .ThenInclude(ua => ua.UserAddress)
                .Include(ui => ui.UserInfo)
                    .ThenInclude(ua => ua.ProfileImage)
                .FirstOrDefaultAsync(u => u.UserName == userName);
            return user;
        }
        public async Task DeleteUserInfo(User userToDelete)//del to nes username tik controleri gaunu
        {
            _context.Users.Remove(userToDelete);
        }
        private void UpsertUserInfo(User currentUser, UserInfo newUserInfo)
        {
            if (currentUser.UserInfo == null)
            {
                currentUser.UserInfo = newUserInfo;
            }
            else
            {
                currentUser.UserInfo.FirstName = newUserInfo.FirstName;
                currentUser.UserInfo.SecName = newUserInfo.SecName;
                currentUser.UserInfo.PersonCode = newUserInfo.PersonCode;
                currentUser.UserInfo.Email = newUserInfo.Email;
                currentUser.UserInfo.PhoneNumber = newUserInfo.PhoneNumber;
            }
        }
        private void UpsertUserAddress(User currentUser, UserAddress newUserAddress)
        {
            if (currentUser.UserInfo.UserAddress == null)
            {
                currentUser.UserInfo.UserAddress = newUserAddress;
            }
            else
            {
                currentUser.UserInfo.UserAddress.City = newUserAddress.City;
                currentUser.UserInfo.UserAddress.Street = newUserAddress.Street;
                currentUser.UserInfo.UserAddress.HouseNmber = newUserAddress.HouseNmber;
                currentUser.UserInfo.UserAddress.ApartamentNumber = newUserAddress.ApartamentNumber;
            }
        }
    }
}
