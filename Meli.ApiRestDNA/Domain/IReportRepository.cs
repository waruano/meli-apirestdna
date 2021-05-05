using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Domain
{
    public interface IReportRepository
    {
        Task SaveReport(Report report);
    }
}
