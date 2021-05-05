using System;
using System.Collections.Generic;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Application.Queries;
using Meli.ApiRestDNA.Domain;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Application.Queries
{
    public class StatsQueryHandlerTests
    {
        private readonly StatsQueryHandler _queryHandler;
        private readonly Mock<IReportFinder> _reportFinder;
        public StatsQueryHandlerTests()
        {
            _reportFinder = new Mock<IReportFinder>();
            _queryHandler = new StatsQueryHandler(_reportFinder.Object);
        }

        [Fact]
        public async Task Handle_WhenTrigger_ShouldCallGetStatsAsync()
        { 
            await _queryHandler.Handle(new StatsQuery(), default);
            _reportFinder.Verify(finder => finder.GetStatsAsync());
        }
    }
}
