using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static HumanRegisterWebApi.Enums.Enums;

namespace HumanRegisterWebApi.Models
{
    public class User
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public byte[] Password { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Column(TypeName = "nvarchar(20)")]
        public Role AppRole { get; set; }
        //Relations
        public UserInfo? UserInfo { get; set; }

        public User()
        {
            
        }

    }
}
