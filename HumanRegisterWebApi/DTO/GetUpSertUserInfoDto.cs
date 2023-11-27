using System.ComponentModel.DataAnnotations;

namespace HumanRegisterWebApi.DTO
{
    public class GetUpSertUserInfoDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecName { get; set; } = string.Empty;
        public long PersonCode { get; set; } = long.MinValue;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
    }
}
