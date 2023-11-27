using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Adapters
{
    public interface IUserInfoAdapter
    {
        UserInfo Bind(GetUpSertUserInfoDto dto);
        GetUpSertUserInfoDto Bind(UserInfo userInfo);
    }
}