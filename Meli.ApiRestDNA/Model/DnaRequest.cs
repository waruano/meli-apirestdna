using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Meli.ApiRestDNA.Domain.Extensions;

namespace Meli.ApiRestDNA.Model
{
    public class DnaRequest
    {
        /// <summary>
        /// un array de Strings que representan cada fila de una tabla de(NxN)
        /// con la secuencia del ADN.Las letras de los Strings solo pueden ser:
        /// (A, T, C, G), las cuales representa cada base nitrogenada del ADN.
        /// </summary>
        public List<string> Dna { get; set; }
    }

    public class DnaRequestValidator : AbstractValidator<DnaRequest>
    {
        private readonly List<string> _validCharacters = new List<string> { "A", "T", "C", "G" };
        public DnaRequestValidator()
        {
            RuleFor(command => command.Dna)
                .NotEmpty()
                .Must(BeValidDna)
                .WithMessage("invalid DNA format");
        }

        private bool BeValidDna(List<string> dna)
        {
            var size = dna.Count;
            return dna.TrueForAll(item => item.Length == size &&
                                          item.TransformDna().ToList()
                                              .TrueForAll(itemChar => _validCharacters.Contains(itemChar.ToString())));
        }

    }
}
