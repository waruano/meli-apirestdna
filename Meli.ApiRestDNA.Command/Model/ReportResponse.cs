using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Meli.ApiRestDNA.Model
{
    public class ReportResponse
    {
        [JsonPropertyName("count_mutant_dna")]
        public long CountMutantDna { get; set; }
        [JsonPropertyName("count_human_dna")]
        public long CountHumanDna { get; set; }
        [JsonIgnore]
        public double Ratio { get; set; }
        [JsonPropertyName("ratio")]
        public double RoundedRatio => Math.Round(Ratio, 2);
    }
}
