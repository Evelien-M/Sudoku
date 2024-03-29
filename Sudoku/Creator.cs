﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    class Creator
    {
        public int[,] Get()
        {
            int[,] grid = new int[9, 9];
            List<string> lines = new List<string>();



            /*        lines.Add("+-------+-------+-------+");
                    lines.Add("| 0 3 0 | 0 1 0 | 0 6 0 |");
                    lines.Add("| 7 5 0 | 0 3 0 | 0 4 8 |");
                    lines.Add("| 0 0 6 | 9 8 4 | 3 0 0 |");
                    lines.Add("+-------+-------+-------+");
                    lines.Add("| 0 0 3 | 0 0 0 | 8 0 0 |");
                    lines.Add("| 9 1 2 | 0 0 0 | 6 7 4 |");
                    lines.Add("| 0 0 4 | 0 0 0 | 5 0 0 |");
                    lines.Add("+-------+-------+-------+");
                    lines.Add("| 0 0 1 | 6 7 5 | 2 0 0 |");
                    lines.Add("| 6 8 0 | 0 9 0 | 0 1 5 |");
                    lines.Add("| 0 9 0 | 0 4 0 | 0 3 0 |");
                    lines.Add("+-------+-------+-------+");*/

            /*           lines.Add("+-------+-------+-------+");
                       lines.Add("| 2 1 8 | 0 0 5 | 0 0 0 |");
                       lines.Add("| 0 9 0 | 6 4 0 | 0 5 0 |");
                       lines.Add("| 0 4 0 | 0 0 0 | 0 0 0 |");
                       lines.Add("+-------+-------+-------+");
                       lines.Add("| 3 0 0 | 9 0 0 | 0 0 2 |");
                       lines.Add("| 1 0 0 | 0 8 0 | 6 0 0 |");
                       lines.Add("| 0 0 2 | 5 1 3 | 0 0 7 |");
                       lines.Add("+-------+-------+-------+");
                       lines.Add("| 0 0 0 | 8 0 0 | 1 0 4 |");
                       lines.Add("| 0 3 0 | 0 0 0 | 0 2 0 |");
                       lines.Add("| 0 2 0 | 0 0 1 | 0 9 5 |");
                       lines.Add("+-------+-------+-------+");*/

            lines.Add("+-------+-------+-------+");
            lines.Add("| 0 0 0 | 0 0 5 | 0 1 0 |");
            lines.Add("| 2 0 0 | 7 9 0 | 5 0 0 |");
            lines.Add("| 0 0 6 | 0 0 1 | 0 0 0 |");
            lines.Add("+-------+-------+-------+");
            lines.Add("| 0 0 0 | 0 0 0 | 6 3 0 |");
            lines.Add("| 7 0 5 | 6 0 2 | 9 0 4 |");
            lines.Add("| 0 3 4 | 0 0 0 | 0 0 0 |");
            lines.Add("+-------+-------+-------+");
            lines.Add("| 0 0 0 | 3 0 0 | 7 0 0 |");
            lines.Add("| 0 0 2 | 0 7 4 | 0 0 9 |");
            lines.Add("| 0 9 0 | 1 0 0 | 0 0 0 |");
            lines.Add("+-------+-------+-------+");

            /*            lines.Add("+-------+-------+-------+");
                        lines.Add("| 0 0 0 | 0 0 5 | 0 4 0 |");
                        lines.Add("| 2 0 0 | 7 9 0 | 5 0 0 |");
                        lines.Add("| 0 0 6 | 0 0 1 | 0 0 0 |");
                        lines.Add("+-------+-------+-------+");
                        lines.Add("| 0 0 0 | 0 0 0 | 6 3 0 |");
                        lines.Add("| 7 0 5 | 6 0 2 | 9 0 4 |");
                        lines.Add("| 0 3 4 | 0 0 0 | 0 0 0 |");
                        lines.Add("+-------+-------+-------+");
                        lines.Add("| 0 0 0 | 3 0 0 | 7 0 0 |");
                        lines.Add("| 0 0 2 | 0 7 4 | 0 0 9 |");
                        lines.Add("| 0 9 0 | 1 0 0 | 0 0 0 |");
                        lines.Add("+-------+-------+-------+");*/

            int y = 0;
            foreach (string line in lines)
            {
                if (line.Equals("+-------+-------+-------+"))
                    continue;

                var arr = line.ToCharArray();
  
                int x = 0;
                for (int j = 0; j < arr.Length; j++)
                {
                    string ch = arr[j].ToString();

                    if (ch.Equals("|") || ch.Equals(" ") || ch.Equals("-") || ch.Equals("+"))
                    {
                        continue;
                    }
                    else
                    {
                        int number = int.Parse(ch);
                        grid[y, x] = number;
                        x++;
                    }
                }       
                y++;
            }
            return grid;
        }

    }
}
