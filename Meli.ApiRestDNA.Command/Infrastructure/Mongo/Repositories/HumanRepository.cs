using System.Threading.Tasks;
using Mapster;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;

namespace Meli.ApiRestDNA.Infrastructure.Mongo.Repositories
{
    public class HumanRepository : IHumanRepository
    {
        private readonly IMongoDbContext<HumanDocument> _dbContext;

        public HumanRepository(IMongoDbContext<HumanDocument> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveAsync(Human human)
        {
            await _dbContext.InsertOneAsync(human.Adapt<HumanDocument>());
        }
    }
}
