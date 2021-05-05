using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Meli.ApiRestDNA.Application;
using Meli.ApiRestDNA.Application.Commands;
using Meli.ApiRestDNA.Controllers.v1;
using Meli.ApiRestDNA.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Controllers.v1
{
    public class MutantControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly MutantController _mutantController;

        public MutantControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mutantController = new MutantController(_mediator.Object);
        }

        [Fact]
        public async Task PostAsync_WhenCalled_ShouldReturnOk()
        {
            _mediator.Setup(mediator => mediator.Send(It.IsAny<DnaValidatorCommand>(), default))
                .ReturnsAsync(true);
            var result = await _mutantController.PostAsync(new DnaRequest(){Dna = new List<string>()});
            Assert.IsType<OkResult>(result);
            _mediator.Verify(mediator => mediator.Send(It.IsAny<DnaValidatorCommand>(), default));
        }

    }
}
