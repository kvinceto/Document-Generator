using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Data;
using DocGen.Dtos.ClientDtos;

using Microsoft.EntityFrameworkCore;

namespace DocGen.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IModelFactory modelFactory;
        private readonly DocGenDbContext dbContext;

        public ClientService(IModelFactory modelFactory, DocGenDbContext dbContext)
        {
            this.modelFactory = modelFactory;
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateClientAsync(ClientDtoAdd clientDto)
        {
            bool exists = await CheckDoesTheClientExistsInTheDatabaseAsync(clientDto.Id);

            if (exists)
            {
                throw new EntityAlreadyExistsException();
            }

            try
            {
                var client = modelFactory.CreateClient(clientDto);

                await dbContext.Clients.AddAsync(client);
                await dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditClientDataAsync(ClientDtoAdd clientDto)
        {
            bool exists = await CheckDoesTheClientExistsInTheDatabaseAsync(clientDto.Id);

            if (!exists)
            {
                throw new EntityDoNotExistsException();
            }

            try
            {
                var client = await dbContext.Clients
                    .FirstAsync(c => c.Id == clientDto.Id);

                client.Id = clientDto.Id;
                client.Name = clientDto.Name;
                client.Address = clientDto.Address;
                client.ContactName = clientDto.ContactName;

                await dbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ClientDtoInfo> GetClientInfoAsync(string clientId)
        {
            bool exist = await CheckDoesTheClientExistsInTheDatabaseAsync(clientId);

            if (!exist)
            {
                throw new EntityDoNotExistsException();
            }

            var client = await dbContext.Clients
                .FirstAsync(c => c.Id == clientId);

            return new ClientDtoInfo()
            {
                Id = clientId,
                Name = client.Name,
                Address = client.Address,
                ContactName = client.ContactName,
                IsDeleted = client.IsDeleted,
                Info = client.Info
            };
        }

        public async Task<bool> DeleteClientAsync(string clientId)
        {
            bool exists = await CheckDoesTheClientExistsInTheDatabaseAsync(clientId);

            if (!exists)
            {
                throw new EntityDoNotExistsException();
            }

            var client = await dbContext.Clients
                .FirstAsync(c => c.Id == clientId);

            client.IsDeleted = true;

            await dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<ICollection<ClientShortInfoDto>> GetAllActiveAndDeletedClientsAsync()
        {
            return await dbContext.Clients
               .Select(c => new ClientShortInfoDto()
               {
                   Id = c.Id,
                   Name = c.Name
               })
               .OrderBy(c => c.Name)
               .ToListAsync();
        }

        public async Task<ICollection<ClientShortInfoDto>> GetAllActiveClientsAsync()
        {
            return await dbContext.Clients
                .Where(c => c.IsDeleted == false)
                .Select(c => new ClientShortInfoDto()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<ICollection<ClientShortInfoDto>> GetAllDeletedClientsAsync()
        {
            return await dbContext.Clients
                .Where(c => c.IsDeleted == true)
                .Select(c => new ClientShortInfoDto()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        private async Task<bool> CheckDoesTheClientExistsInTheDatabaseAsync(string clientId)
        {
            return await dbContext.Clients
                            .AnyAsync(c => c.Id == clientId);
        }
    }
}
