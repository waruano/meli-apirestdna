using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Infrastructure.Mongo.Repositories;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Infrastructure.Mongo.Repositories
{
    public class ReportRepositoryTests
    {
        private readonly Mock<IMongoDbContext<ReportDocument>> _dbContext;
        private readonly ReportRepository _reportRepository;
        public ReportRepositoryTests()
        {
            _dbContext = new Mock<IMongoDbContext<ReportDocument>>();
            _reportRepository = new ReportRepository(_dbContext.Object);
        }

        [Fact]
        public async Task SaveReport_WhenReportIsNew_ShouldCallInsertOneAsync()
        {
            var humanReport = new Report(1, 1);
            await _reportRepository.SaveReport(humanReport);
            _dbContext.Verify(context => context.InsertOneAsync(It.Is<ReportDocument>(
                report => report.CountHumanDna == humanReport.CountHumanDna && report.CountMutantDna == humanReport.CountMutantDna )));
        }

        [Fact]
        public async Task SaveReport_WhenReportAlreadyExist_ShouldCallInsertOneAsync()
        {
            var humanReport = new Report(1, 1, ObjectId.GenerateNewId().ToString());
            await _reportRepository.SaveReport(humanReport);
            _dbContext.Verify(context => context.ReplaceOneAsync(It.Is<ReportDocument>(
                report => report.CountHumanDna == humanReport.CountHumanDna && report.CountMutantDna == humanReport.CountMutantDna
                && report.Id.ToString() == humanReport.Id)));
        }
    }
}
