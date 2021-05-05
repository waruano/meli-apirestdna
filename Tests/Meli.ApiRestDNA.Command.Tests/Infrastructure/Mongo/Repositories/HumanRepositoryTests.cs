using System.Collections.Generic;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Infrastructure.Mongo.Repositories;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Infrastructure.Mongo.Repositories
{
    public class HumanRepositoryTests
    {
        private readonly Mock<IMongoDbContext<HumanDocument>> _dbContext;
        private readonly HumanRepository _humanRepository;
        public HumanRepositoryTests()
        {
            _dbContext = new Mock<IMongoDbContext<HumanDocument>>();
            _humanRepository = new HumanRepository(_dbContext.Object);
        }

        [Fact]
        public async Task SaveAsync_WhenCalled_ShouldCallInsertOneAsync()
        {
            var human = new Human(new List<string> { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" },true);
            await _humanRepository.SaveAsync(human);
            _dbContext.Verify(context => context.InsertOneAsync(It.IsAny<HumanDocument>()));
        }
    }
}
