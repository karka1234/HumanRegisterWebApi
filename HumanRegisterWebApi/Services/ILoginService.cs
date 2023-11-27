using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;

namespace HumanRegisterWebApi.Services
{
    public interface ILoginService
    {
        User CreateUser(string userName, string password);
        LoginResponseDTO Login(string userName, string password);
        User RegisterNewUser(string userName, string password);
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}