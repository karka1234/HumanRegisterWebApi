using HumanRegisterWebApi.Enums;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Services
{
    public interface IJwtService
    {
        //string GetJwtToken(string username, string userId, Role role);
        string GetJwtToken(string username, Role role);
    }
}