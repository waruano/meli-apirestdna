using Meli.ApiRestDNA.Shared.Infrastructure.Mongo;

namespace Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel
{
    [BsonCollection("Report")]
    public class ReportDocument : Document
    {
        public long CountMutantDna { get; set; }
        public long CountHumanDna { get; set; }
        public double Ratio { get; set; }
    }
}
