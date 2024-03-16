using DocGen.Data.Models;
using DocGen.Dtos.ClientDtos;
using DocGen.Dtos.CompanyDtos;
using DocGen.Dtos.InvoiceDtos;
using DocGen.Dtos.UserDtos;

using Microsoft.AspNetCore.Identity;

namespace DocGen.Core.Contracts
{
    public interface IModelFactory
    {
        Company CreateCompany(CompanyDtoAdd companyDto);

        Client CreateClient(ClientDtoAdd clientDto);

        Invoice CreateInvoice(InvoiceDtoAdd invoiceDto);

        IdentityUser CreateUser(UserRegisterDto registerDto);
    }
}
