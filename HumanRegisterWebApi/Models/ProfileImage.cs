using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanRegisterWebApi.Models
{
    public class ProfileImage
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public byte[] ImageBytes { get; set; }

        //relations
        [ForeignKey("UserInfo")]
        public Guid? UserInfoId { get; set; }
        public UserInfo? UserInfo { get; set; }
    }
}
