using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Meli.ApiRestDNA.Domain;

namespace Meli.ApiRestDNA.Application.Events
{
    public class SavedHumanEventHandler : INotificationHandler<SavedHumanEvent>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportFinder _reportFinder;

        public SavedHumanEventHandler(IReportRepository reportRepository, IReportFinder reportFinder)
        {
            _reportRepository = reportRepository;
            _reportFinder = reportFinder;
        }

        public async Task Handle(SavedHumanEvent notification, CancellationToken cancellationToken)
        {
            var report = await _reportFinder.GetStatsAsync();
            if (report == null)
            {
                report = new Report(notification.IsMutant ? 1 : 0,1);
            }
            else
            {
                report.AddHuman(notification.IsMutant);
            }
            await _reportRepository.SaveReport(report);
        }
    }
}
