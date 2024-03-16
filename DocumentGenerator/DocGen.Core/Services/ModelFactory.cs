using DocGen.Core.Contracts;
using DocGen.Data.Models;
using DocGen.Dtos.ClientDtos;
using DocGen.Dtos.CompanyDtos;
using DocGen.Dtos.InvoiceDtos;
using DocGen.Dtos.UserDtos;

using Microsoft.AspNetCore.Identity;

namespace DocGen.Core.Services
{
    public class ModelFactory : IModelFactory
    {
        public Client CreateClient(ClientDtoAdd clientDto)
        {
            return new Client()
            {
                Id = clientDto.Id,
                Name = clientDto.Name,
                Address = clientDto.Address,
                ContactName = clientDto.ContactName,
                Info = clientDto.Info,
                IsDeleted = clientDto.IsDeleted
            };
        }

        public Company CreateCompany(CompanyDtoAdd companyDto)
        {
            return new Company()
            {
                Id = companyDto.Id,
                Name = companyDto.Name,
                Address = companyDto.Address,
                ContactName = companyDto.ContactName,
                Info = companyDto.Info,
                IsDeleted = companyDto.IsDeleted
            };
        }

        public Invoice CreateInvoice(InvoiceDtoAdd invoiceDto)
        {
            return new Invoice()
            {
                InvoiceNumber = invoiceDto.InvoiceNumber,
                CompanyId = invoiceDto.CompanyId,
                ClientId = invoiceDto.ClientId,
                DateOfIsue = DateTime.Parse(invoiceDto.DateOfIsue),
                DateOfServiceProvided = DateTime.Parse(invoiceDto.DateOfServiceProvided),
                DueDate = DateTime.Parse(invoiceDto.DueDate),
                ShipingInfo = invoiceDto.ShipingInfo,
                Notes = invoiceDto.Notes,
                Subtotal = invoiceDto.Subtotal,
                Discount = invoiceDto.Discount,
                Tax = invoiceDto.Tax,
                Total = invoiceDto.Total,
                TextContent = invoiceDto.TextContent,
                PaymentMethod = invoiceDto.PaymentMethod,
                UserId = invoiceDto.UserId,
                Currency = invoiceDto.Currency
            };
        }

        public IdentityUser CreateUser(UserRegisterDto registerDto)
        {
            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerDto.Email,
                NormalizedUserName = registerDto.Email.ToUpper(),
                Email = registerDto.Email,
                EmailConfirmed = true,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                NormalizedEmail = registerDto.Email.ToUpper(),
                PhoneNumber = registerDto.PhoneNumber,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                TwoFactorEnabled = false,
            };

            var hasher = new PasswordHasher<IdentityUser>();

            user.PasswordHash = hasher.HashPassword(user, registerDto.Password);

            return user;
        }
    }
}
