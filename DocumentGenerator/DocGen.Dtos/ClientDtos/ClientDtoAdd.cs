using System.ComponentModel.DataAnnotations;

using static DocGen.Common.EntityValidationConstants.ClientEntity;

namespace DocGen.Dtos.ClientDtos
{
    public class ClientDtoAdd
    {
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
        public string Address { get; set; } = null!;

        [Required]
        [StringLength(ContactNameMaxLength, MinimumLength = ContactNameMinLength)]
        public string ContactName { get; set; } = null!;

        public string? Info { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
