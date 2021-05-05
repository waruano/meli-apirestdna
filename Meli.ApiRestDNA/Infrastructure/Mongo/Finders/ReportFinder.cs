using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Mapster;

namespace Meli.ApiRestDNA.Infrastructure.Mongo.Finders
{
    public class ReportFinder : IReportFinder
    {
        private readonly IMongoDbContext<ReportDocument> _dbContext;

        public ReportFinder(IMongoDbContext<ReportDocument> dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Report> GetStatsAsync()
        {
            Expression<Func<ReportDocument, bool>> filter = document => (true);
            var humanReportDocument = await _dbContext.FindOneAsync(filter);
            return humanReportDocument.Adapt<Report>();
        }
    }
}
