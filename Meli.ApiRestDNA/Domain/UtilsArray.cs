using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meli.ApiRestDNA.Domain
{
    public static class UtilsArray
    {
        public static async Task<List<string>> GetVerticals(List<string> dna)
        {
            var size = dna.Count;
            var result = new char[size][];
            
            await Task.Run(() => {
                for (var i = 0; i < size; i++)
                {
                    var itemRow = dna[i].ToCharArray();
                    for (var j = 0; j < (size +1)/ 2; j++)
                    {
                        var jUp = size - j - 1;
                        if (result[j] == null)
                        {
                            result[j] = new char[size];
                            result[j][0] = itemRow[j];
                        }
                        else
                        {
                            result[j][i] = itemRow[j];
                        }

                        if (jUp <= j) continue;
                        if (result[jUp] == null)
                        {
                            result[jUp] = new char[size];
                            result[jUp][0] = itemRow[jUp];
                        }
                        else
                        {
                            result[jUp][i] = itemRow[jUp];
                        }
                    }
                }
            });
            return result.Select(item => new string(item)).ToList();
        }

        public static async Task<List<string>> GetObliques(List<string> dna)
        {
            var size = dna.Count;
            var result = new List<string>();
            await Task.Run(() =>
            {
                for (var i = 0; i < size - 3; i++)
                {
                    var j = 0;
                    var k = i;
                    var diagonalCharsCol = new List<char>();
                    var diagonalCharsRow = new List<char>();
                    var diagonalCharsColInv = new List<char>();
                    var diagonalCharsRowInv = new List<char>();
                    while (k < size && j < size)
                    {
                        var kUp = size - 1 - k;
                        var jUp = size - 1 - j;
                        diagonalCharsCol.Add(dna[k].ToCharArray()[j]);
                        diagonalCharsColInv.Add(dna[k].ToCharArray()[jUp]);
                        if (k != j)
                        {
                            diagonalCharsRow.Add(dna[j].ToCharArray()[k]);
                            diagonalCharsRowInv.Add(dna[j].ToCharArray()[kUp]);
                        }

                        k++;
                        j++;
                    }

                    result.Add(new string(diagonalCharsCol.ToArray()));
                    result.Add(new string(diagonalCharsColInv.ToArray()));
                    if (diagonalCharsRow.Count > 0)
                        result.Add(new string(diagonalCharsRow.ToArray()));
                    if (diagonalCharsRowInv.Count > 0)
                        result.Add(new string(diagonalCharsRowInv.ToArray()));
                }
            });
            return result;
        }
    }
}
