using System.ComponentModel.DataAnnotations;

namespace Identity.IdentityCore.JWT.Models
{
    public class UserDTO
    {
        [Required]
        public  string UsertName { get; set; }
        [Required]
        public  string Password { get; set; }
    }
}
