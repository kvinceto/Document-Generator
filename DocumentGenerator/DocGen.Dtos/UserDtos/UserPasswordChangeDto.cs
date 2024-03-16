using System.ComponentModel.DataAnnotations;

namespace DocGen.Dtos.UserDtos
{
    public class UserPasswordChangeDto
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string OldPassword { get; set; } = null!;

        [Required]
        public string NewPassword { get; set; } = null!;

        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = null!;
    }
}
