using System.ComponentModel.DataAnnotations;

namespace DocGen.Dtos.UserDtos
{
    public class UserLoginDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }
}
