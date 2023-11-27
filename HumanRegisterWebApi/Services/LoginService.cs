using HumanRegisterWebApi.Database;
using HumanRegisterWebApi.DTO;
using HumanRegisterWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Services
{
    public class LoginService : ILoginService
    //atkirai viskas nuo main programos funkcionalumo 
    {
        private readonly AppDbContext _context;
        public LoginService(AppDbContext context)
        {
            _context = context;
        }
        public User RegisterNewUser(string userName, string password)
        {
            if (!CheckUserNameExist(userName))
                return null;
            var user = CreateUser(userName, password);
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }



        public LoginResponseDTO Login(string userName, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.UserName == userName);
            if (user == null)
                return new LoginResponseDTO(false);
            var isSuccess = VerifyPasswordHash(password, user.Password, user.PasswordSalt);
            return new LoginResponseDTO(isSuccess, user.AppRole);
        }

        public User CreateUser(string userName, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
            return new User
            {
                UserName = userName,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                AppRole = Role.User
            };
        }



        private bool CheckUserNameExist(string userName)
        {
            return GetUserByUserName(userName) == null;
        }
        private User GetUserByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA256(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
