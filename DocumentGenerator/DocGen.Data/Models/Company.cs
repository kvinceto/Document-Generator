using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

using static DocGen.Common.EntityValidationConstants.CompanyEntity;

namespace DocGen.Data.Models
{
    public class Company
    {
        [Key]
        [Unicode(true)]
        public string Id { get; set; } = null!;

        [Required]
        [Unicode(true)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [Unicode(true)]
        [MaxLength(AddressMaxLength)]
        public string Address { get; set; } = null!;

        [Required]
        [Unicode(true)]
        [MaxLength(ContactNameMaxLength)]
        public string ContactName { get; set; } = null!;

        [Unicode(true)]
        public string? Info { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; } = new HashSet<Invoice>();
    }
}
