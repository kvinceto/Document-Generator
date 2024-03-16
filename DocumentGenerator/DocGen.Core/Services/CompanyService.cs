using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Data;
using DocGen.Dtos.CompanyDtos;

using Microsoft.EntityFrameworkCore;

namespace DocGen.Core.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly DocGenDbContext dbContext;
        private readonly IModelFactory modelFactory;

        public CompanyService(DocGenDbContext dbContext, IModelFactory modelFactory)
        {
            this.dbContext = dbContext;
            this.modelFactory = modelFactory;
        }

        public async Task<bool> CreateCompanyAsync(CompanyDtoAdd companyDto)
        {
            bool exists = await CheckDoesTheCompanyExistsInTheDatabaseAsync(companyDto.Id);

            if (exists)
            {
                throw new EntityAlreadyExistsException();
            }

            try
            {
                var company = modelFactory.CreateCompany(companyDto);

                await dbContext.Companies.AddAsync(company);
                await dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditCompanyDataAsync(CompanyDtoEdit companyDto)
        {
            bool exists = await CheckDoesTheCompanyExistsInTheDatabaseAsync(companyDto.Id);

            if (!exists)
            {
                throw new EntityDoNotExistsException();
            }

            try
            {
                var company = await dbContext.Companies
                    .FirstAsync(c => c.Id == companyDto.Id);

                company.Id = companyDto.Id;
                company.Name = companyDto.Name;
                company.Address = companyDto.Address;
                company.ContactName = companyDto.ContactName;

                await dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<CompanyDtoInfo> GetCompanyInfoAsync(string companyId)
        {
            bool exist = await CheckDoesTheCompanyExistsInTheDatabaseAsync(companyId);

            if (!exist)
            {
                throw new EntityDoNotExistsException();
            }

            var company = await dbContext.Companies
                .FirstAsync(c => c.Id == companyId);

            return new CompanyDtoInfo()
            {
                Id = companyId,
                Name = company.Name,
                Address = company.Address,
                ContactName = company.ContactName,
                IsDeleted = company.IsDeleted,
                Info = company.Info
            };
        }

        public async Task<bool> DeleteCompanyAsync(string companyId)
        {
            bool exists = await CheckDoesTheCompanyExistsInTheDatabaseAsync(companyId);

            if (!exists)
            {
                throw new EntityDoNotExistsException();
            }

            var company = await dbContext.Companies
                .FirstAsync(c => c.Id == companyId);

            company.IsDeleted = true;

            await dbContext.SaveChangesAsync();

            return true;
        }

        private async Task<bool> CheckDoesTheCompanyExistsInTheDatabaseAsync(string companyId)
        {
            return await dbContext.Companies
                            .AnyAsync(c => c.Id == companyId);
        }

        public async Task<ICollection<CompanyShortInfoDto>> GetAllActiveCompaniesAsync()
        {
            return await dbContext.Companies
                .Where(c => c.IsDeleted == false)
                .Select(c => new CompanyShortInfoDto()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<ICollection<CompanyShortInfoDto>> GetAllDeletedCompaniesAsync()
        {
            return await dbContext.Companies
                .Where(c => c.IsDeleted == true)
                .Select(c => new CompanyShortInfoDto()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<ICollection<CompanyShortInfoDto>> GetAllActiveAndDeletedCompaniesAsync()
        {
            return await dbContext.Companies
               .Select(c => new CompanyShortInfoDto()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .OrderBy(c => c.Name)
               .ToListAsync();
        }
    }
}
