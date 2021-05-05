using System.Collections.Generic;
using Meli.ApiRestDNA.Model;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Model
{
    public class DnaRequestTests
    {
        private readonly List<string> _dnaValid = new List<string> { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" };
        public static IEnumerable<object[]> InValidDnaItems =>
            new List<object[]>
            {
                new object[]
                {
                    new List<string> { "ATGCG", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" }
                },
                new object[]
                {
                    new List<string> { "ATzCA", "TAGAT", "CCCTT", "CAGAG", "GGTTC" }
                }
            };
        [Fact]
        public void AccessorsAndMutators_WhenCalled_ShouldBeValid()
        {
            var dnaRequest = new DnaRequest
            {
                Dna = _dnaValid
            };
            Assert.Equal(dnaRequest.Dna, _dnaValid);
        }

        [Fact]
        public void DneRequestValidator_WhenObjectIsValid_ShouldReturnIsValid()
        {
            var validator = new DnaRequestValidator();
            var dnaRequest = new DnaRequest
            {
                Dna = _dnaValid
            };
            var validateResult = validator.Validate(dnaRequest);
            Assert.True(validateResult.IsValid);
        }

        [Theory]
        [MemberData(nameof(InValidDnaItems))]
        public void DneRequestValidator_WhenObjectIsInValid_ShouldReturnIsValid(List<string> dna)
        {
            var validator = new DnaRequestValidator();
            var dnaRequest = new DnaRequest
            {
                Dna = dna
            };
            var validateResult = validator.Validate(dnaRequest);
            Assert.False(validateResult.IsValid);
        }
    }
}
