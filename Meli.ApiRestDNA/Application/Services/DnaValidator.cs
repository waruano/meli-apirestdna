using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Domain;

namespace Meli.ApiRestDNA.Application.Services
{
    public class DnaValidator : IDnaValidator
    {
        private readonly List<string> _itemsToSearch = new List<string>()
        {
            "AAAA","TTTT","CCCC","GGGG"
        };
        public async Task<bool> IsMutant(List<string> dna)
        {
            var total = dna.Count(item => _itemsToSearch.Where(item.Contains).Any());
            if (total > 1)
                return true;
            var verticals = await UtilsArray.GetVerticals(dna);
            total += verticals.Count(item => _itemsToSearch.Where(item.Contains).Any());
            if (total > 1)
                return true;
            var obliques = await UtilsArray.GetObliques(dna);
            total += obliques.Count(item => _itemsToSearch.Where(item.Contains).Any());
            return total > 1;
        }
    }
}
