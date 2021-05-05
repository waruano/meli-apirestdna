namespace Meli.ApiRestDNA.Domain
{
    public class Report
    {
        public Report()
        {}
        public Report(long countMutantDna, long countHumanDna, string id = null)
        {
            CountMutantDna = countMutantDna;
            CountHumanDna = countHumanDna;
            Ratio = (double)CountMutantDna / CountHumanDna;
            Id = id;
        }

        public void AddHuman(bool isMutant)
        {
            CountHumanDna++;
            if (isMutant)
                CountMutantDna++;
            Ratio = (double)CountMutantDna / CountHumanDna;
        }
        public string Id { get; set; }
        public long CountMutantDna { get; set; }
        public long CountHumanDna { get; set; }
        public double Ratio { get; set; }
    }
}
