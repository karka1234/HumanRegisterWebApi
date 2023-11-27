using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HumanRegisterWebApi.DTO
{
    public class GetUpsertNoImageDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string SecName { get; set; } = string.Empty;
        public long PersonCode { get; set; } = long.MinValue;
        public string Email { get; set; } = string.Empty;
        public long PhoneNumber { get; set; }
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int HouseNmber { get; set; }
        public int ApartamentNumber { get; set; }
    }
}
