using DocGen.Common.CustomClases;
using DocGen.Core.Contracts;
using DocGen.Core.Services;
using DocGen.Data;
using DocGen.Dtos.ClientDtos;

using Microsoft.EntityFrameworkCore;

namespace DocGen.Tests
{
    [TestFixture]
    public class ClientServiseTests
    {
        private DocGenDbContext dbContext;
        private DbContextOptions<DocGenDbContext> options;
        private IModelFactory modelFactory = new ModelFactory();
        private IClientService clientService;

        [SetUp]
        public async Task Setup()
        {
            this.options = new DbContextOptionsBuilder<DocGenDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            this.dbContext = new DocGenDbContext(this.options);

            this.clientService = new ClientService(this.modelFactory, dbContext);

            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

        }

        [Test]
        public async Task CreateClientAsyncCreatesClient()
        {
            ClientDtoAdd dtoAdd = new ClientDtoAdd()
            {
                Id = "test name",
                Name = "Test",
                ContactName = "Test",
                Address = "Test",
                IsDeleted = false,
                Info = "Test"
            };

            bool result = await clientService.CreateClientAsync(dtoAdd);

            Assert.IsTrue(result);

            var client = await dbContext.Clients.FirstOrDefaultAsync(c => c.Id == dtoAdd.Id);

            Assert.IsNotNull(client);

            Assert.That(dtoAdd.Id, Is.EqualTo(client.Id));
            Assert.That(dtoAdd.Name, Is.EqualTo(client.Name));
            Assert.That(dtoAdd.ContactName, Is.EqualTo(client.ContactName));
            Assert.That(dtoAdd.Address, Is.EqualTo(client.Address));
            Assert.That(dtoAdd.IsDeleted, Is.EqualTo(client.IsDeleted));
            Assert.That(dtoAdd.Info, Is.EqualTo(client.Info));

        }

        [Test]
        public async Task CreateClientAsyncReturnsCustomError()
        {
            ClientDtoAdd dtoAdd = new ClientDtoAdd()
            {
                Id = "test name",
                Name = "Test",
                ContactName = "Test",
                Address = "Test",
                IsDeleted = false,
                Info = "Test"
            };

            bool result = await clientService.CreateClientAsync(dtoAdd);
            Assert.True(result);

            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () =>
            await clientService.CreateClientAsync(dtoAdd));
        }

        [TearDown]
        public async Task TearDown()
        {
            await this.dbContext.Database.EnsureDeletedAsync();
            this.dbContext.Dispose();
        }
    }
}