using System;
using System.Text.Json.Serialization;

namespace Meli.ApiRestDNA.Model
{
    public class ReportResponse
    {
        /// <summary>
        /// numero total de mutantes
        /// </summary>
        [JsonPropertyName("count_mutant_dna")]
        public long CountMutantDna { get; set; }

        /// <summary>
        /// numero total de humanos evaluados
        /// </summary>
        [JsonPropertyName("count_human_dna")]
        public long CountHumanDna { get; set; }
        [JsonIgnore]
        public double Ratio { get; set; }

        /// <summary>
        /// Relacion entre mutantes y humanos
        /// </summary>
        [JsonPropertyName("ratio")]
        public double RoundedRatio => Math.Round(Ratio, 2);
    }
}
