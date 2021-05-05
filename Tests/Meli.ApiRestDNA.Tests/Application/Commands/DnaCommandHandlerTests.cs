using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Meli.ApiRestDNA.Application.Commands;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Domain.Exceptions;
using Moq;
using Xunit;

namespace Meli.ApiRestDNA.Tests.Application.Commands
{
    public class DnaCommandHandlerTests
    {
        private readonly Mock<IDnaValidator> _dnaValidator;
        private readonly Mock<IHumanRepository> _humanRepository;
        private readonly Mock<IMediator> _mediator;
        private readonly DnaValidatorCommandHandler _commandHandler;
        public DnaCommandHandlerTests()
        {
            _humanRepository = new Mock<IHumanRepository>();
            _dnaValidator = new Mock<IDnaValidator>();
            _mediator = new Mock<IMediator>();
            _commandHandler = new DnaValidatorCommandHandler(_dnaValidator.Object, _humanRepository.Object, _mediator.Object);
        }
        [Fact]
        public async Task Handle_WhenHumanIsMutant_ShouldReturnTrue()
        {
            _dnaValidator.Setup(validator => validator.IsMutant(It.IsAny<List<string>>()))
                .ReturnsAsync(true);
            var result = await _commandHandler.Handle(new DnaValidatorCommand(new List<string>()), default);
            Assert.True(result);
            _dnaValidator.Verify(validator => validator.IsMutant(It.IsAny<List<string>>()));
            _humanRepository.Verify(repository => repository.SaveAsync(It.IsAny<Human>()));
        }

        [Fact]
        public async Task Handle_WhenHumanIsNotMutant_ShouldThrowMutantNotFound()
        {
            _dnaValidator.Setup(validator => validator.IsMutant(It.IsAny<List<string>>()))
                .ReturnsAsync(false);
            await Assert.ThrowsAsync<MutantNotFound>(()=> _commandHandler.Handle(new DnaValidatorCommand(new List<string>()), default));
            _dnaValidator.Verify(validator => validator.IsMutant(It.IsAny<List<string>>()));
            _humanRepository.Verify(repository => repository.SaveAsync(It.IsAny<Human>()));
        }

    }
}
