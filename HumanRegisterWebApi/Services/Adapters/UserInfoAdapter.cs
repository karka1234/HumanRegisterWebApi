using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Adapters
{
    public class UserInfoAdapter : IUserInfoAdapter
    {
        public GetUpSertUserInfoDto Bind(UserInfo userInfo)
        {
            return new GetUpSertUserInfoDto()
            {
                FirstName = userInfo.FirstName,
                SecName = userInfo.SecName,
                PersonCode = userInfo.PersonCode,
                Email = userInfo.Email,
                PhoneNumber = userInfo.PhoneNumber,
            };
        }

        public UserInfo Bind(GetUpSertUserInfoDto dto)
        {
            return new UserInfo()
            {
                FirstName = dto.FirstName,
                SecName = dto.SecName,
                PersonCode = dto.PersonCode,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
            };
        }
    }
}
