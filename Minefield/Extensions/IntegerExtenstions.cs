using System;
namespace Minefield.Extensions
{
    public static class IntegerExtenstions
    {
        static readonly char[] Columns = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        public static char ToChar(this int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException("index must be a positive number");

            return Columns[index];
        }
    }
}
