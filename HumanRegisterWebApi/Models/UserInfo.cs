using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanRegisterWebApi.Models
{
    public class UserInfo
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string SecName { get; set; } = string.Empty;
        [Required]
        [MaxLength(11)]
        public long PersonCode { get; set; } = long.MinValue;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        [Required]
        [MaxLength(12)]
        public long PhoneNumber { get; set; }
        //relations
        [ForeignKey("User")]
        public Guid? UserId { get; set; }
        public User? User { get; set; }


        //public Guid? UserAddressId {  get; set; }
        public UserAddress? UserAddress { get; set; }
        //public Guid? ProfileImageId { get; set; }
        public ProfileImage? ProfileImage { get; set; }
    }
}
