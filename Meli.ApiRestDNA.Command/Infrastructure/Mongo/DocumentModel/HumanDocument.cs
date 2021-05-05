using System.Collections.Generic;
using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;

namespace Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel
{
    [BsonCollection("Human")]
    public class HumanDocument : Document
    {
        public bool IsMutant { get; set; }
        public List<string> Dna { get; set; }
    }
}
