using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Domain
{
    public interface IDnaValidator
    {
        public Task<bool> IsMutant(List<string> dna);
    }
}
