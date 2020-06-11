using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sudoku
{
    public class View
    {
        public static void Write(int[,] matrix, string highlight = null)
        {
            int a = (int)Math.Sqrt(Math.Sqrt(matrix.Length));
            int b = 0;
            string write = "";

           write += "+---------+---------+---------+\n";
            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                if (y == a)
                {
                    write += "+---------+---------+---------+\n";
                    a = a + a;
                }
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (x == 0)
                    {
                        write += "|";
                    }
                    if (b == 3)
                    {
                        write += "|";
                        b = 0;
                    }
                    if (highlight != null && highlight.Equals(y + "" + x))
                    {
                        // parse it??
                        write += " " + matrix[y, x] + " ";
                    }
                    else
                    {
                        write += " " + matrix[y, x] + " ";
                    }
                    if (x == matrix.GetLength(1) - 1)
                    {
                        write += "|";
                    }
                    b++;    
                }
                b = 0;
                write += "\n";
            }
            write += "+---------+---------+---------+\n";
            Console.WriteLine(write);
            Thread.Sleep(100);
        }
    }
}
