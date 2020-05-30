using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class View
    {
        public static void Write(int[,] matrix)
        {
            int a = (int)Math.Sqrt(Math.Sqrt(matrix.Length));
            int b = 0;

            Console.WriteLine("-------------------------------");
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                if (y == a)
                {
                    Console.WriteLine("-------------------------------");
                    a = a + a;
                }
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (x == 0)
                    {
                        Console.Write("|");
                    }
                    if (b == 3)
                    {
                        Console.Write("|");
                        b = 0;
                    }
                    Console.Write(" " + matrix[y, x] + " ");
                    if (x == matrix.GetLength(1) - 1)
                    {
                        Console.Write("|");
                    }
                    b++;
                }
                b = 0;

                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
