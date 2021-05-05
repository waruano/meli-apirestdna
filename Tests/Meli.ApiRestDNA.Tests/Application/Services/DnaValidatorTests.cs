using System.Collections.Generic;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Application.Services;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Application.Services
{
    public class DnaValidatorTests
    {
        private readonly DnaValidator _dnaValidator;

        public DnaValidatorTests()
        {
            _dnaValidator = new DnaValidator();
        }

        [Fact]
        public async Task IsMutant_WhenDnaIsMutant_ShouldReturnTrue()
        {
            var dna = new List<string> { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
            var result = await _dnaValidator.IsMutant(dna);
            Assert.True(result);
        }

        [Fact]
        public async Task IsMutant_WhenDnaIsNotMutant_ShouldReturnFalse()
        {
            var dna = new List<string> { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" };
            var result = await _dnaValidator.IsMutant(dna);
            Assert.False(result);
        }
    }
}
