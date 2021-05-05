using System.Collections.Generic;

namespace Meli.ApiRestDNA.Domain
{
    public class Human
    {
        public Human(List<string> dna, bool isMutant = false, string id = null)
        {
            Dna = dna;
            IsMutant = isMutant;
            Id = id;
        }

        public List<string> Dna { get; private set; }
        public bool IsMutant { get; private set; }
        public string Id { get; private set; }

    }
}
