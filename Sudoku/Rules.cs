using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class Rules
    {
        private int[,] grid;
        private Dictionary<string, List<int>> grids;

        public Rules(int[,] grid)
        {
            this.grid = grid;
            this.CreateGrid();
        }

        public bool CheckValid(int y, int x, int input)
        {
            if (this.CheckHorizontal(y, input) && this.CheckVertical(x, input) && this.CheckGrid(y, x, input))
                return true;

            return false;
        }

        private bool CheckHorizontal(int y, int input)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                if (this.grid[y, x] == input)
                {
                    if (Program.feedback)
                        Console.WriteLine("Cannot place " + input + " due to a horizontal constrain on: " + this.grid[y, x]);
                    return false;
                }
            }
            return true;
        }
        private bool CheckVertical(int x, int input)
        {
            for (int y = 0; y < this.grid.GetLength(0); y++)
            {
                if (this.grid[y, x] == input)
                {
                    if (Program.feedback)
                        Console.WriteLine("Cannot place " + input +" due to a vertical constrain on: " + this.grid[y, x]);
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
                    if (Program.feedback)
                    {
                        Console.WriteLine("Cannot place " + input + " due to a grid constrain on: " + input);
                    }
                    return false;
                }
            }

            return true;
        }

        private void CreateGrid()
        {
            this.grids = new Dictionary<string, List<int>>();
            for (int y = 0; y < this.grid.GetLength(0); y++)
            {
                for (int x = 0; x < this.grid.GetLength(1); x++)
                {
                    if (this.grid[y, x] != 0)
                    {
                        this.AddToGrid(y, x, this.grid[y, x]);
                    }
                }
            }
        }

        public void AddToGrid(int y, int x, int value)
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

        public void RemoveFromGrid(int y, int x, int value)
        {
            int hX = x / 3;
            int vY = y / 3;
            string key = hX + "" + vY;

            if (this.grids.ContainsKey(key))
            {
                this.grids[key].Remove(value);
            }
        }
    }
}
