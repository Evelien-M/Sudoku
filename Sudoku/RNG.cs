using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    static class RNG
    {
        private static readonly System.Random _random = new System.Random();

        public static void Shuffle<T>(List<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = _random.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }
    }
}
