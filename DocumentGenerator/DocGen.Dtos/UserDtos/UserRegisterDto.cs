using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DocGen.Dtos.UserDtos
{
    public class UserRegisterDto
    {

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password))]
        [JsonPropertyName("repass")]
        public string ConfirmPassword { get; set; } = null!;

        [JsonPropertyName("phone")]
        public string? PhoneNumber { get; set; }
    }
}
