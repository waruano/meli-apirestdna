using System.Collections.Generic;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Application.Services;
using Meli.ApiRestDNA.Domain.Exceptions;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Application.Services
{
    public class DnaValidatorByCharTests
    {
        private readonly DnaValidatorByChar _dnaValidator;

        public DnaValidatorByCharTests()
        {
            _dnaValidator = new DnaValidatorByChar();
        }

        public static IEnumerable<object[]> ValidDnaItems =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string> { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
                },
                new object[]
                {
                    new List<string> { "AAAAA", "TTCCA", "TTTTA", "CCAAG" }
                },
                new object[]
                {
                    new List<string> { "AAAAAAAA", "ATATATAT", "GCGCGCGC", "ATATATAT", "GCGCGCGC", "ATATATAT", "GCGCGCGC", "ATATATAT" }
                },
                new object[]
                {
                    new List<string> { "ATGCA", "TAGAT", "CCATT", "CAGAG", "GGTTC" }
                },
                new object[]
                {
                    new List<string> { "ATGCa", "TCcAT", "ccATT", "CAgAG", "ggttc " }
                }
            };

        public static IEnumerable<object[]> InValidDnaItems =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string> { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" }
                },
                new object[]
                {
                    new List<string> { "ATGCA", "TAGAT", "CCCTT", "CAGAG", "GGTTC" }
                }
            };

        [Theory]
        [MemberData(nameof(ValidDnaItems))]
        public async Task IsMutant_WhenDnaIsMutant_ShouldReturnTrue(List<string> dna)
        {
            var result = await _dnaValidator.IsMutant(dna);
            Assert.True(result);
        }

        [Theory]
        [MemberData(nameof(InValidDnaItems))]
        public async Task IsMutant_WhenDnaIsNotMutant_ShouldReturnFalse(List<string> dna)
        {
            var result = await _dnaValidator.IsMutant(dna);
            Assert.False(result);
        }

    }
}
