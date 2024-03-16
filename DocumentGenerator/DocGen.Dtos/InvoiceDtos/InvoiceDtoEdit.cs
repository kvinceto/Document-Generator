using System.ComponentModel.DataAnnotations;

namespace DocGen.Dtos.InvoiceDtos
{
    public class InvoiceDtoEdit : InvoiceDtoAdd
    {
        [Required]
        public int Id { get; set; }
    }
}
