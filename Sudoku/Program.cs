using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new Creator();
            var grid = c.Get();
            View.Write(grid);
            Console.WriteLine("Preparing...");

            new Calculator(grid);

            Console.ReadLine();
        }
    }
}
