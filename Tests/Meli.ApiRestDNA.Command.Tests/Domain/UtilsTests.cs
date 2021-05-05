using System.Collections.Generic;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Domain;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Domain
{
    public class UtilsTests
    {
        public UtilsTests()
        {

        }

        [Fact]
        public async Task GetVerticals_WhenNxNMatrix_ShouldReturnTranspose()
        {
            var dna = new List<string>
            {
                "ABCDEF",
                "GHIJKL",
                "MNOPQR",
                "STUVWX",
                "YZ1234",
                "567890"
            };
            var transpose = new List<string>
            {
                "AGMSY5",
                "BHNTZ6",
                "CIOU17",
                "DJPV28",
                "EKQW39",
                "FLRX40",
            };

            var result = await UtilsArray.GetVerticals(dna);

            Assert.Equal(transpose, result);
        }

        [Fact]
        public async Task Oblique_WhenNxNMatrix_ShouldReturnOblique()
        {
            var dna = new List<string>
            {
                "ABCDEF",
                "GHIJKL",
                "MNOPQR",
                "STUVWX",
                "YZ1234",
                "567890"
            };
            var oblique = new List<string>
            {
                "AHOV30",
                "FKPUZ5",
                "GNU29",
                "LQV16",
                "BIPW4",
                "EJOTY",
                "MT18",
                "RW27",
                "CJQX",
                "DINS"
            };
            var result = await UtilsArray.GetObliques(dna);
            Assert.Equal(result,oblique);
        }
    }
}
