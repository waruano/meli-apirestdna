using System.Collections.Generic;
using MediatR;

namespace Meli.ApiRestDNA.Application.Commands
{
    public class DnaValidatorCommand : IRequest<bool>
    {
        public List<string> DnaList { get; set; }

        public DnaValidatorCommand(List<string> dnaList)
        {
            DnaList = dnaList;
        }
    }
}
