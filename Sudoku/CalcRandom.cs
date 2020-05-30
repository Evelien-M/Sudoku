using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Sudoku
{
    public class CalcRandom
    {
        private bool running;
        private int[,] input;
        private int[,] output;
        private int attemps;
        private int[] allInputs;
        private Dictionary<string, List<int>> grids;
        private Random rng;
        private Moves tile;

        private bool feedback = false;
        private bool rngSlow = true;

        public CalcRandom(int[,] grid)
        {
            Console.WriteLine("Preparing...");
            this.input = grid;

            this.output = this.input.Clone() as int[,];

            this.rng = new Random();
            this.tile = new Moves();
            this.CreateGrid();


            this.allInputs = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            this.CreatePossible();
            tile.UpdateAmount();
            this.running = true;

            // this.attemps = 1;
            //        this.attemps = 1000000000;

      

            

        }

        private void CreateGrid()
        {
            this.grids = new Dictionary<string, List<int>>();
            for (int y = 0; y < this.output.GetLength(0); y++)
            {
                for (int x = 0; x < this.output.GetLength(1); x++)
                {
                    if (this.output[y, x] != 0)
                    {
                        this.AddToGrid(y, x, this.output[y, x]);
                    }
                }
            }
        }

        private void AddToGrid(int y, int x, int value)
        {
            int hX = x / 3;
            int vY = y / 3;
            string key = hX + "" + vY;

            if (this.grids.ContainsKey(key))
            {
                this.grids[key].Add(value);
            }
            else
            {
                var l = new List<int>();
                l.Add(value);
                this.grids.Add(key, l);
            }
        }

        public void Start(object parameter)
        {
            Console.WriteLine("Working...");
            while (!Algorithm.succeeded)
            {
                if (!this.Check())
                {
                    this.output = this.input.Clone() as int[,];
                    this.CreateGrid();
             //       tile.Next();
                }
                else
                {
                    Console.WriteLine("Sollution found!");
                    Algorithm.output = output;
                    Algorithm.succeeded = true;
                }
            }
        }

        private bool Check()
        {
            for (int y = 0; y < this.output.GetLength(0); y++)
            {
                for (int x = 0; x < this.output.GetLength(1); x++)
                {
                    if (this.output[y, x] == 0)
                    {
                        List<int> nmb;
                        if (rngSlow)
                        {
                            nmb = this.tile.GetRandom(y, x);
                        }
                        else
                        {
                            nmb = this.tile.Get(y, x);
                        }

                        for (int j = 0; j < nmb.Count; j++)
                        {
                            bool valid = this.CheckValid(y, x, nmb[j]);
                            if (j == nmb.Count - 1 && !valid) // All possible moves are not valid 
                            {
                                if (feedback)
                                {
                                    Console.WriteLine("Failed: Placing " + nmb[j] + " on " + x + "X - " + y + "Y");
                                    Console.WriteLine("Failed: The map is invalid");
                                }
                                return false;
                            } 

                            if (valid)
                            {
                                this.output[y, x] = nmb[j];
                                this.AddToGrid(y, x, nmb[j]);
                                if (feedback)
                                {
                                    Console.WriteLine("Succes: Placing " + nmb[j] + " on " + x + "X - " + y + "Y");
                                    View.Write(this.output);
                                    Thread.Sleep(1000);
                                }
                                break;
                            }
                            else if (feedback)
                            {
                                Console.WriteLine("Failed: Placing " + nmb[j] + " on " + x + "X - " + y + "Y");
                            }
                        }
                    }
                }
            }
            return true;
        }

        private bool CheckValid(int y, int x, int input)
        {
            if (this.CheckHorizontal(y,input) && this.CheckVertical(x,input) && this.CheckGrid(y,x,input))
                return true;

            return false;
        }

        private bool CheckHorizontal(int y, int input)
        {
            for (int x = 0; x < this.output.GetLength(1); x++)
            {
                if (this.output[y, x] == input)
                {
                    if (feedback)
                        Console.WriteLine("Horizontal constrain on: " + this.output[y, x]);
                    return false;
                }
            }
            return true;
        }
        private bool CheckVertical(int x, int input)
        {
            for (int y = 0; y < this.output.GetLength(0); y++)
            {
                if (this.output[y, x] == input)
                {
                    if (feedback)
                        Console.WriteLine("Vertical constrain on: " + this.output[y, x]);
                    return false;
                }
            }
            return true;
        }

        private bool CheckGrid(int y, int x, int input)
        {
            int hX = x / 3;
            int vY = y / 3;
            string key = hX + "" + vY;

            if (this.grids.ContainsKey(key))
            {
                var l = this.grids[key];
                if (l.Contains(input))
                {
                    if (this.feedback)
                    {
                        Console.WriteLine("Grid constrain on: " + input);
                    }
                    return false;
                }
            }

            return true;
        }

        private void CreatePossible()
        {
            for (int y = 0; y < this.output.GetLength(0); y++)
            {
                for (int x = 0; x < this.output.GetLength(1); x++)
                {
                    if (this.output[y, x] == 0)
                    {
                        for (int i = 1; i < 10; i++)
                        {
                            if (this.CheckValid(y, x, i))
                            {
                                tile.Add(y, x, i);
                            }
                        }
                    }
                }
            }
        }
    }
}
