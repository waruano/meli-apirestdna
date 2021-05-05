using System.Collections.Generic;
using Mapster;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Infrastructure.Mongo.DocumentModel;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Infrastructure.Mongo.DocumentModel
{
    public class HumanDocumentTests
    {
        [Fact]
        public void Adapt_WhenCalled_ShouldCreateHumanDocument()
        {
            var human = new Human(new List<string> { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }, true);
            var humanDocument = human.Adapt<HumanDocument>();
            Assert.Equal(human.IsMutant, humanDocument.IsMutant);
            Assert.Equal(human.Dna, humanDocument.Dna);
        }
    }
}
