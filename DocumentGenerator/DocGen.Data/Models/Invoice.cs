using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DocGen.Data.Models
{
    public class Invoice
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Unicode(true)]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        [Unicode(true)]
        public string CompanyId { get; set; } = null!;

        public virtual Company Company { get; set; } = null!;

        [Required]
        [Unicode(true)]
        public string ClientId { get; set; } = null!;

        public virtual Client Client { get; set; } = null!;

        [Unicode(true)]
        public string? ShipingInfo { get; set; }

        [Required]
        public DateTime DateOfIsue { get; set; }

        [Required]
        public DateTime DateOfServiceProvided { get; set; }

        public DateTime DueDate { get; set; }

        [Unicode(true)]
        public string TextContent { get; set; } = null!;

        [Required]
        public decimal Subtotal { get; set; }

        [Required]
        public decimal Discount { get; set; }

        [Required]
        public decimal Tax { get; set; }

        [Required]
        public decimal Total { get; set; }

        [Required]
        public string Currency { get; set; } = null!;

        [Required]
        public string PaymentMethod { get; set; } = null!;

        [Unicode(true)]
        public string? Notes { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;

        public virtual IdentityUser User { get; set; } = null!;
    }
}
