using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meli.ApiRestDNA.Domain;
using Meli.ApiRestDNA.Domain.Exceptions;
using Meli.ApiRestDNA.Domain.Extensions;

namespace Meli.ApiRestDNA.Application.Services
{
    public class DnaValidatorByChar : IDnaValidator
    {
        private const int MatchLength = 4;

        public async Task<bool> IsMutant(List<string> dna)
        {
            try
            {
                await FindAllDirections(dna);
            }
            catch (MutantFound)
            {
                return true;
            }
            return false;
        }

        private static void CountOccurrences(ref string currentString, ref int count, char item)
        {
            if (string.IsNullOrEmpty(currentString) || currentString.ToCharArray().Last() == item)
            {
                currentString += item;
                if (currentString.Length < MatchLength) return;
                count++;
                if (count > 1) throw new MutantFound();
                currentString = string.Empty;
            }
            else
            {
                currentString = string.Empty;
            }
        }

        private static async Task FindAllDirections(IReadOnlyList<string> dna)
        {
            var occurrences = 0;
            var size = dna.Count;
            var resultVertical = new string[size];
            var resultHorizontal = new string[size];

            await Task.Run(() =>
            {
                for (var i = 0; i < size; i++)
                {
                    var k = i;
                    var diagonalCharsCol = string.Empty;
                    var diagonalCharsRow = string.Empty;
                    var diagonalCharsColInv = string.Empty;
                    var diagonalCharsRowInv = string.Empty;

                    for (var j = 0; j < size; j++ , k++)
                    {

                        if (i < size - (MatchLength - 1) && k < size)
                        {
                            var kUp = size - 1 - k;
                            var jUp = size - 1 - j;
                            CountOccurrences(ref diagonalCharsCol, ref occurrences, dna[k].TransformDna()[j]);
                            CountOccurrences(ref diagonalCharsColInv, ref occurrences, dna[k].TransformDna()[jUp]);

                            if (k != j)
                            {
                                CountOccurrences(ref diagonalCharsRow, ref occurrences, dna[j].TransformDna()[k]);
                                CountOccurrences(ref diagonalCharsRowInv, ref occurrences, dna[j].TransformDna()[kUp]);
                            }
                        }
                        CountOccurrences(ref resultVertical[j], ref occurrences, dna[i].TransformDna()[j]);
                        CountOccurrences(ref resultHorizontal[j], ref occurrences, dna[j].TransformDna()[i]);
                    }
                }
            });
        }
    }
}
