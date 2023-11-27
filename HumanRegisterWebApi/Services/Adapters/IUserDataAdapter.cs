using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Adapters
{
    public interface IUserDataAdapter
    {
        GetUpsertDTO Bind(UserInfo userInfo, UserAddress userAddress, ProfileImage profileImage);
        User Bind(GetUpsertNoImageDTO data);
    }
}