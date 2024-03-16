using DocGen.Dtos.InvoiceDtos;

namespace DocGen.Core.Contracts
{
    public interface IInvoiceService
    {
        Task<bool> CreateInvoiceAsync(InvoiceDtoAdd invoiceDto);

        Task<bool> EditInvoiceAsync(InvoiceDtoEdit invoiceDto);

        Task<bool> DeleteInvoiceAsync(int id);

        Task<InvoiceDtoFullInfo> GetInvoiceFullInfo(int id);

        Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByCompany(string companyId);

        Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByClient(string clientId);

        Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByUser(string userId);
    }
}
