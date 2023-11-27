using System.ComponentModel.DataAnnotations;

namespace HumanRegisterWebApi.DTO
{
    public class GetUpsertUserAddressDto
    {
        public string City { get; set; } = string.Empty;

        public string Street { get; set; } = string.Empty;

        public int HouseNmber { get; set; }

        public int ApartamentNumber { get; set; }
    }
}
