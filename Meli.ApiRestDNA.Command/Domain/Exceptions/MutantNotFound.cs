using Meli.ApiRestDNA.Shared.Exceptions;

namespace Meli.ApiRestDNA.Domain.Exceptions
{
    public class MutantNotFound : BusinessException
    {
        public MutantNotFound()
            : base($"is a Human")
        { }
        public override int ErrorCode => 403;
    }
}
