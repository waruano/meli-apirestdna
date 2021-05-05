using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Meli.ApiRestDNA.Application.Queries;
using Meli.ApiRestDNA.Controllers.v1;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Model;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Controllers.v1
{
    public class StatsControllerTests
    {
        private Mock<IMediator> _mediator;
        private StatsController _statsController;

        public StatsControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _statsController = new StatsController(_mediator.Object);
        }

        [Fact]
        public async Task GetAsync_WhenHumanReportIsNotNull_ShouldReturnReportResponse()
        {
            var humanReport = new Report(1, 1, ObjectId.GenerateNewId().ToString());
            _mediator.Setup(mediator => mediator.Send(It.IsAny<StatsQuery>(), default))
                .ReturnsAsync(humanReport);
            var result = await _statsController.GetAsync();
            Assert.IsType<OkObjectResult>(result);
            _mediator.Verify(mediator => mediator.Send(It.IsAny<StatsQuery>(),default));
            var objectResult = (OkObjectResult) result;
            var reportResponse = (ReportResponse) objectResult.Value;
            Assert.Equal(humanReport.CountHumanDna, reportResponse.CountHumanDna);
            Assert.Equal(humanReport.CountMutantDna,reportResponse.CountMutantDna);
            Assert.Equal(humanReport.Ratio,reportResponse.Ratio);
        }
    }
}
