using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Meli.ApiRestDNA.Application.Events;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Domain.Exceptions;

namespace Meli.ApiRestDNA.Application.Commands
{
    public class DnaValidatorCommandHandler : IRequestHandler<DnaValidatorCommand, bool>
    {
        private readonly IDnaValidator _dnaValidator;
        private readonly IHumanRepository _humanRepository;
        private readonly IMediator _mediator;

        public DnaValidatorCommandHandler(IDnaValidator dnaValidator, IHumanRepository humanRepository, IMediator mediator)
        {
            _dnaValidator = dnaValidator;
            _humanRepository = humanRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DnaValidatorCommand request, CancellationToken cancellationToken)
        {
            var isMutant = await _dnaValidator.IsMutant(request.DnaList);
            var human = new Human(request.DnaList, isMutant);
            await _humanRepository.SaveAsync(human);
            await _mediator.Publish(new SavedHumanEvent() {IsMutant = isMutant}, cancellationToken);
            if (!isMutant)
            {
                throw new MutantNotFound();
            }
            return true;
        }
    }
}
