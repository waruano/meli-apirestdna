namespace Meli.ApiRestDNA.Domain.Extensions
{
    public static class DnaExtension
    {
        /// <summary>
        /// Transform string to char array, Upper Case and Trim
        /// </summary>
        /// <returns></returns>
        public static char[] TransformDna(this string dnaItem)
        {
            return dnaItem.ToUpper().Trim().ToCharArray();
        }
    }
}
