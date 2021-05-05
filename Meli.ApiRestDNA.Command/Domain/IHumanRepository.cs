using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Domain
{
    public interface IHumanRepository
    {
        Task SaveAsync(Human human);
    }
}
