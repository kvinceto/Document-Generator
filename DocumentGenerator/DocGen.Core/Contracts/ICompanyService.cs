using DocGen.Dtos.CompanyDtos;

namespace DocGen.Core.Contracts
{
    public interface ICompanyService
    {
        Task<bool> CreateCompanyAsync(CompanyDtoAdd companyDto);

        Task<bool> EditCompanyDataAsync(CompanyDtoEdit companyDto);

        Task<CompanyDtoInfo> GetCompanyInfoAsync(string companyId);

        Task<bool> DeleteCompanyAsync(string companyId);

        Task<ICollection<CompanyShortInfoDto>> GetAllActiveCompaniesAsync();

        Task<ICollection<CompanyShortInfoDto>> GetAllDeletedCompaniesAsync();

        Task<ICollection<CompanyShortInfoDto>> GetAllActiveAndDeletedCompaniesAsync();
    }
}
