using Mapster;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Meli.ApiRestDNA.Infrastructure.Mongo.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoDbContext<ReportDocument> _dbContext;

        public ReportRepository(IMongoDbContext<ReportDocument> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task SaveReport(Report report)
        {
            var reportDocument = report.Adapt<ReportDocument>();
            if (reportDocument.Id == ObjectId.Empty)
            {
                await _dbContext.InsertOneAsync(reportDocument);
            }
            else
            {
                await _dbContext.ReplaceOneAsync(reportDocument);
            }
        }
    }
}
