using MediatR;
using Meli.ApiRestDNA.Domain;

namespace Meli.ApiRestDNA.Application.Queries
{
    public class StatsQuery : IRequest<Report>
    {
        public StatsQuery()
        {

        }
    }
}
