using System.ComponentModel.DataAnnotations;

namespace DocGen.Dtos.InvoiceDtos
{
    public class InvoiceDtoAdd
    {
        [Required]
        public string InvoiceNumber { get; set; } = null!;

        [Required]
        public string CompanyId { get; set; } = null!;

        [Required]
        public string ClientId { get; set; } = null!;

        public string? ShipingInfo { get; set; }

        [Required]
        public string DateOfIsue { get; set; } = null!;

        [Required]
        public string DateOfServiceProvided { get; set; } = null!;

        [Required]
        public string DueDate { get; set; } = null!;

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

        public string? Notes { get; set; }

        [Required]
        public string UserId { get; set; } = null!;
    }
}
