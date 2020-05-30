using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Algorithm
    {
        public static bool succeeded;
        public static bool failed;
        private int[,] grid;
        public static int[,] output;

        public Algorithm(int[,] grid)
        {
            this.grid = grid;
            Console.WriteLine("Preparing...");
            // this.CallCalc(5);
            Calculator c = new Calculator(grid);

            this.grid = c.output;
            View.Write(this.grid);
            this.CallCalc(5);

            this.CheckSolution();
        }

        private void CheckSolution()
        {
            if (succeeded)
            {
                View.Write(output);
            }
        }

        private void CallCalc(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                CalcRandom c1 = new CalcRandom(grid);
                Thread t = new Thread(new ParameterizedThreadStart(c1.Start));
                t.Start();
            }
        }
    }
}
