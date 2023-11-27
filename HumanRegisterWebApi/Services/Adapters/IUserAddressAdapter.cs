using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services.Adapters
{
    public interface IUserAddressAdapter
    {
        UserAddress Bind(GetUpsertUserAddressDto dto);
        GetUpsertUserAddressDto Bind(UserAddress userAddress);
    }
}