using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Creator();
            var grid = c.Get();

            Calc c1 = new Calc(grid);

            Console.ReadLine();
        }
    }
}
