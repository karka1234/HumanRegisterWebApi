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


/*
 * 
 *         public string FirstName { get; set; } = string.Empty;
        public string SecName { get; set; } = string.Empty;
        public long PersonCode { get; set; } = long.MinValue;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
 * 
 * **/