using MediatR;
using Meli.ApiRestDNA.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Application.Queries
{
    public class StatsQueryHandler : IRequestHandler<StatsQuery, Report>
    {
        private readonly IReportFinder _reportFinder;

        public StatsQueryHandler(IReportFinder reportFinder)
        {
            _reportFinder = reportFinder;
        }

        public async Task<Report> Handle(StatsQuery request, CancellationToken cancellationToken)
        {
            return await _reportFinder.GetStatsAsync();
        }
    }
}
