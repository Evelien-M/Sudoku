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

        public Algorithm(int[,] grid)
        {
            this.grid = grid;

            CalcRandom c1 = new CalcRandom(grid);

            this.CallCalc(2);
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
