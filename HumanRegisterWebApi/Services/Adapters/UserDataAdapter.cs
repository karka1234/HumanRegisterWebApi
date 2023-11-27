using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Adapters
{
    public class UserDataAdapter : IUserDataAdapter
    {
        public GetUpsertDTO Bind(UserInfo userInfo, UserAddress userAddress, ProfileImage profileImage)
        {
            return new GetUpsertDTO()
            {
                FirstName = userInfo.FirstName,
                SecName = userInfo.SecName,
                PersonCode = userInfo.PersonCode,
                Email = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
                City = userAddress.City,
                Street = userAddress.Street,
                HouseNmber = userAddress.HouseNmber,
                ApartamentNumber = userAddress.ApartamentNumber,
                ProfileImageBytes = profileImage.ImageBytes,
            };
        }

        public User Bind(GetUpsertNoImageDTO data)
        {
            return new User()
            {
                UserInfo = new UserInfo
                {
                    FirstName = data.FirstName,
                    SecName = data.SecName,
                    PersonCode = data.PersonCode,
                    Email = data.Email,
                    PhoneNumber = data.PhoneNumber,

                    UserAddress = new UserAddress()
                    {
                        City = data.City,
                        Street = data.Street,
                        HouseNmber = data.HouseNmber,
                        ApartamentNumber = data.ApartamentNumber,
                    },
                }
            };
        }
    }
}
