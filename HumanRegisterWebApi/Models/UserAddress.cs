using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanRegisterWebApi.Models
{
    public class UserAddress
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string City { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string Street { get; set; } = string.Empty;
        [Required]
        [MaxLength(3)]
        public int HouseNmber { get; set; }
        [Required]
        [MaxLength(3)]
        public int ApartamentNumber { get; set; }

        //relations
        [ForeignKey("UserInfo")]
        public Guid? UserInfoId { get; set; }
        public UserInfo? UserInfo { get; set; }
    }
}
