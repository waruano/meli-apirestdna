using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Meli.ApiRestDNA.Infrastructure.Mongo.Finders;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Infrastructure.Mongo.Finders
{
    public class ReportFinderTests
    {
        private readonly Mock<IMongoDbContext<ReportDocument>> _dbContext;
        private ReportFinder _reportFinder;
        public ReportFinderTests()
        {
            _dbContext = new Mock<IMongoDbContext<ReportDocument>>();
            _reportFinder = new ReportFinder(_dbContext.Object);
        }

        [Fact]
        public async Task GetStatsAsync_WhenCalled_ShouldReturnReport()
        {
            var reportDocument = new ReportDocument
            {
                CountHumanDna = 2,
                CountMutantDna = 2,
                Ratio = 1
            };
            _dbContext.Setup(context => context.FindOneAsync(It.IsAny<Expression<Func<ReportDocument, bool>>>()))
                .ReturnsAsync(reportDocument);
            var result = await _reportFinder.GetStatsAsync();
            Assert.Equal(result.CountHumanDna, reportDocument.CountHumanDna);
            Assert.Equal(result.CountMutantDna, reportDocument.CountMutantDna);
            Assert.Equal(result.Ratio, reportDocument.Ratio);
        }
    }
}
