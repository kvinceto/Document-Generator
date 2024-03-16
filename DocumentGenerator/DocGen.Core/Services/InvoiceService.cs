using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Data;
using DocGen.Dtos.InvoiceDtos;

using Microsoft.EntityFrameworkCore;

using static DocGen.Common.ApplicationGlobalConstants;

namespace DocGen.Core.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IModelFactory modelFactory;
        private readonly DocGenDbContext dbContext;

        public InvoiceService(IModelFactory modelFactory, DocGenDbContext dbContext)
        {
            this.modelFactory = modelFactory;
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateInvoiceAsync(InvoiceDtoAdd invoiceDto)
        {
            bool exists = await this.CheckIfInvoiceWithThisNumberExistsForThisCompany(invoiceDto.InvoiceNumber, invoiceDto.CompanyId);

            if (exists)
            {
                throw new EntityAlreadyExistsException();
            }
            try
            {
                var invoice = this.modelFactory.CreateInvoice(invoiceDto);

                await this.dbContext.Invoices.AddAsync(invoice);
                await this.dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditInvoiceAsync(InvoiceDtoEdit invoiceDto)
        {
            bool exists = await this.CheckIfInvoiceWithThisNumberExistsForThisCompany(invoiceDto.InvoiceNumber, invoiceDto.CompanyId);

            if (!exists)
            {
                throw new EntityDoNotExistsException();
            }

            try
            {
                var invoice = await dbContext.Invoices.FirstAsync(i => i.Id == invoiceDto.Id);

                invoice.InvoiceNumber = invoiceDto.InvoiceNumber;
                invoice.CompanyId = invoiceDto.CompanyId;
                invoice.ClientId = invoiceDto.ClientId;
                invoice.DateOfIsue = DateTime.Parse(invoiceDto.DateOfIsue);
                invoice.DateOfServiceProvided = DateTime.Parse(invoiceDto.DateOfServiceProvided);
                invoice.DueDate = DateTime.Parse(invoiceDto.DueDate);
                invoice.TextContent = invoiceDto.TextContent;
                invoiceDto.Currency = invoiceDto.Currency;
                invoice.Subtotal = invoiceDto.Subtotal;
                invoice.Discount = invoiceDto.Discount;
                invoice.Tax = invoiceDto.Tax;
                invoice.Total = invoiceDto.Total;
                invoice.Notes = invoiceDto.Notes;
                invoice.ShipingInfo = invoiceDto.ShipingInfo;
                invoice.UserId = invoiceDto.UserId;

                await this.dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<InvoiceDtoFullInfo> GetInvoiceFullInfo(int id)
        {
            var invoice = await dbContext.Invoices
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                throw new EntityDoNotExistsException();
            }

            return new InvoiceDtoFullInfo()
            {
                Id = id,
                InvoiceNumber = invoice.InvoiceNumber,
                CompanyId = invoice.CompanyId,
                ClientId = invoice.ClientId,
                UserId = invoice.UserId,
                Currency = invoice.Currency,
                Subtotal = invoice.Subtotal,
                Discount = invoice.Discount,
                Tax = invoice.Tax,
                Total = invoice.Total,
                Notes = invoice.Notes,
                ShipingInfo = invoice.ShipingInfo,
                PaymentMethod = invoice.PaymentMethod,
                TextContent = invoice.TextContent,
                DateOfIsue = invoice.DateOfIsue.ToString(DefaultDateFormat),
                DateOfServiceProvided = invoice.DateOfServiceProvided.ToString(DefaultDateFormat),
                DueDate = invoice.DueDate.ToString(DefaultDateFormat)
            };
        }

        public async Task<bool> DeleteInvoiceAsync(int id)
        {
            var invoice = await dbContext.Invoices
                .FirstOrDefaultAsync(i => i.Id == id);

            if (invoice == null)
            {
                throw new EntityDoNotExistsException();
            }

            try
            {
                dbContext.Invoices.Remove(invoice);
                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByClient(string clientId)
        {
            var invoices = await dbContext.Invoices
                .Where(i => i.ClientId == clientId)
                .Select(i => new InvoiceDtoFullInfo
                {
                    Id = i.Id,
                    InvoiceNumber = i.InvoiceNumber,
                    CompanyId = i.CompanyId,
                    ClientId = i.ClientId,
                    UserId = i.UserId,
                    Currency = i.Currency,
                    Subtotal = i.Subtotal,
                    Discount = i.Discount,
                    Tax = i.Tax,
                    Total = i.Total,
                    Notes = i.Notes,
                    ShipingInfo = i.ShipingInfo,
                    PaymentMethod = i.PaymentMethod,
                    TextContent = i.TextContent,
                    DateOfIsue = i.DateOfIsue.ToString(DefaultDateFormat),
                    DateOfServiceProvided = i.DateOfServiceProvided.ToString(DefaultDateFormat),
                    DueDate = i.DueDate.ToString(DefaultDateFormat)
                })
                .ToListAsync();

            return invoices;
        }

        public async Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByCompany(string companyId)
        {
            var invoices = await dbContext.Invoices
               .Where(i => i.CompanyId == companyId)
               .Select(i => new InvoiceDtoFullInfo
               {
                   Id = i.Id,
                   InvoiceNumber = i.InvoiceNumber,
                   CompanyId = i.CompanyId,
                   ClientId = i.ClientId,
                   UserId = i.UserId,
                   Currency = i.Currency,
                   Subtotal = i.Subtotal,
                   Discount = i.Discount,
                   Tax = i.Tax,
                   Total = i.Total,
                   Notes = i.Notes,
                   ShipingInfo = i.ShipingInfo,
                   PaymentMethod = i.PaymentMethod,
                   TextContent = i.TextContent,
                   DateOfIsue = i.DateOfIsue.ToString(DefaultDateFormat),
                   DateOfServiceProvided = i.DateOfServiceProvided.ToString(DefaultDateFormat),
                   DueDate = i.DueDate.ToString(DefaultDateFormat)
               })
               .ToListAsync();

            return invoices;
        }

        public async Task<ICollection<InvoiceDtoFullInfo>> GetAllInvoicesByUser(string userId)
        {
            var invoices = await dbContext.Invoices
               .Where(i => i.UserId == userId)
               .Select(i => new InvoiceDtoFullInfo
               {
                   Id = i.Id,
                   InvoiceNumber = i.InvoiceNumber,
                   CompanyId = i.CompanyId,
                   ClientId = i.ClientId,
                   UserId = i.UserId,
                   Currency = i.Currency,
                   Subtotal = i.Subtotal,
                   Discount = i.Discount,
                   Tax = i.Tax,
                   Total = i.Total,
                   Notes = i.Notes,
                   ShipingInfo = i.ShipingInfo,
                   PaymentMethod = i.PaymentMethod,
                   TextContent = i.TextContent,
                   DateOfIsue = i.DateOfIsue.ToString(DefaultDateFormat),
                   DateOfServiceProvided = i.DateOfServiceProvided.ToString(DefaultDateFormat),
                   DueDate = i.DueDate.ToString(DefaultDateFormat)
               })
               .ToListAsync();

            return invoices;
        }

        private async Task<bool> CheckIfInvoiceWithThisNumberExistsForThisCompany(string invoiceNumber, string companyId)
        {
            return await dbContext.Invoices
                .AnyAsync(i => i.InvoiceNumber == invoiceNumber && i.CompanyId == companyId);
        }
    }
}
