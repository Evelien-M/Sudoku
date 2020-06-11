using System;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace Sudoku
{
    class Program
    {
        public static bool feedback = false;
        static void Main(string[] args)
        {
            var c = new Creator();
            var grid = c.Get();
            Console.WriteLine("Starting grid:");
            View.Write(grid);
            Thread.Sleep(500);
            Console.WriteLine("Preparing...");
            // timer start
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            new Algorithm(grid);
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:000}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds);
            Console.WriteLine("RunTime " + elapsedTime);
         

            Console.ReadLine();
        }


    }
}
