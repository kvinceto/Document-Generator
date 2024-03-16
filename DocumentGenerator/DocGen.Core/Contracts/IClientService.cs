using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocGen.Dtos.ClientDtos;
using DocGen.Dtos.CompanyDtos;

namespace DocGen.Core.Contracts
{
    public interface IClientService
    {
        Task<bool> CreateClientAsync(ClientDtoAdd clientDto);

        Task<bool> EditClientDataAsync(ClientDtoAdd clientDto);

        Task<ClientDtoInfo> GetClientInfoAsync(string clientId);

        Task<bool> DeleteClientAsync(string clientId);

        Task<ICollection<ClientShortInfoDto>> GetAllActiveClientsAsync();

        Task<ICollection<ClientShortInfoDto>> GetAllDeletedClientsAsync();

        Task<ICollection<ClientShortInfoDto>> GetAllActiveAndDeletedClientsAsync();
    }
}
