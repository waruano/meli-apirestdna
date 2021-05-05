using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Application.Events;
using Meli.ApiRestDNA.Domain;
using MongoDB.Bson;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Application.Events
{
    public class SavedHumanEventHandlerTests
    {
        private readonly SavedHumanEventHandler _eventHandler;
        private readonly Mock<IReportRepository> _reportRepository;
        private readonly Mock<IReportFinder> _reportFinder;
        public SavedHumanEventHandlerTests()
        {
            _reportFinder = new Mock<IReportFinder>();
            _reportRepository = new Mock<IReportRepository>();
            _eventHandler = new SavedHumanEventHandler(_reportRepository.Object, _reportFinder.Object);
        }

        [Fact]
        public async Task Handle_WhenReportIsNull_ShouldCreateNewReport()
        {
            await _eventHandler.Handle(new SavedHumanEvent {IsMutant = true}, default);
            _reportFinder.Verify(finder => finder.GetStatsAsync());
            _reportRepository.Verify(repository => repository.SaveReport(It.IsAny<Report>()));
        }

        [Fact]
        public async Task Handle_WhenReportIsAny_ShouldCreateNewReport()
        {
            _reportFinder.Setup(finder => finder.GetStatsAsync())
                .ReturnsAsync(new Report(1, 1, ObjectId.GenerateNewId().ToString()));
            await _eventHandler.Handle(new SavedHumanEvent { IsMutant = true }, default);
            _reportFinder.Verify(finder => finder.GetStatsAsync());
            _reportRepository.Verify(repository => repository.SaveReport(It.Is<Report>( report => 
                report.CountHumanDna == 2 && report.CountMutantDna == 2 && Math.Abs(report.Ratio - 1.0) < 0.1)));
        }
    }
}
