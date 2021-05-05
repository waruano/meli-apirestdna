using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Domain
{
    public interface IReportFinder
    {
        Task<Report> GetStatsAsync();
    }
}
