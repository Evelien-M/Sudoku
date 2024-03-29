﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Algorithm
    {
        private int steps;
        private int[,] input;
        public int[,] output;
        private Rules rules;

        private bool fillup;
        SortedDictionary<string, LinkedMoves> possibleMoves;
        private string[] order;
        private LinkedMoves start;
        private LinkedMoves end;

        public Algorithm(int[,] grid)
        {
            this.input = grid;
            this.output = this.input.Clone() as int[,];
            this.rules = new Rules(this.output);

            this.possibleMoves = new SortedDictionary<string, LinkedMoves>();

            this.Start();
        }

        public void Start()
        {
            this.fillup = true;
            if (Program.feedback)
            {
                Console.WriteLine("Filling up all the one move set tiles...");
            }
            while (fillup)
            {
                this.CreatePossible();
                this.UpdatePossible();
                if (Program.feedback)
                {
                    Console.WriteLine("Step " + this.steps);
                    View.Write(this.output);
                    Thread.Sleep(400);
                }
            }

            this.CreatePossible();

            if (this.possibleMoves.Count == 0)
            {
                if (this.IsCompleted())
                {
                    Console.WriteLine("Solution found!(" + steps + ")");
                    View.Write(this.output);
                }
                else
                {
                    Console.WriteLine("It is impossible!");
                }
                return;
            }

            Console.WriteLine("Working...");
            this.SortAmountPossibleMoves();
            this.CreateLinkedList();
            this.LoopThrough();
        }

        private bool IsCompleted()
        {
            for (int y = 0; y < this.output.GetLength(0); y++)
            {
                for (int x = 0; x < this.output.GetLength(1); x++)
                {
                    if (this.output[y, x] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        #region Possibles
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
                            if (this.rules.CheckValid(y, x, i))
                            {
                                this.AddPossible(y, x, i);
                            }
                        }
                    }
                }
            }
        }

        private void AddPossible(int y, int x, int value)
        {
            string key = y + "" + x;
            if (this.possibleMoves.ContainsKey(key))
            {
                var m = this.possibleMoves[key];
                m.Moves.Add(value);
            }
            else
            {
                var l = new List<int>();
                l.Add(value);
                var m = new LinkedMoves(key, l);
                this.possibleMoves.Add(key, m);
            }
            this.steps++;
        }

        private void UpdatePossible()
        {
            bool changes = false;
            foreach (KeyValuePair<string, LinkedMoves> entry in this.possibleMoves)
            {
                var a = entry.Value.Moves;
                if (a.Count == 1)
                {
                    var key = entry.Key;
                    var value = entry.Value.Moves[0];
                    int y = Int32.Parse(key[0].ToString());
                    int x = Int32.Parse(key[1].ToString());
                    this.output[y, x] = value;
                    changes = true;
                }
            }
            this.possibleMoves = new SortedDictionary<string, LinkedMoves>();
            this.fillup = changes;
        }
        #endregion

        private void SortAmountPossibleMoves()
        {
            if (Program.feedback)
            {
                Console.WriteLine("Sorting...");
            }
            this.order = new string[this.possibleMoves.Count];

            var a = this.possibleMoves.OrderBy(o => o.Value.Moves.Count);
            var b = a.Select(s => s.Key).ToArray();
            this.order = b;
        }

        private void CreateLinkedList()
        {
            for (int i = 0; i < this.order.Length; i++)
            {
                if (Program.feedback)
                        Console.Write(this.order[i] + "------>");

                if (this.start == null)
                {
                    this.end = this.possibleMoves[order[0]];
                    this.start = this.end;
                }
                else
                {
                    var temp = this.end;
                    this.end = this.possibleMoves[order[i]];
                    this.end.Previous = temp;
                    temp.Next = this.end;
                }
            }

            if (Program.feedback)
            {
                Console.WriteLine();
                Thread.Sleep(1000);
            }
        }

        private void LoopThrough()
        {
            var next = this.start;
            string placed = null;
            
            while (next != null)
            {
                if (Program.feedback)
                {
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("Step " + steps);
                    Console.WriteLine("(" + next.Y + "" + next.X + ") Amount of moves: " + next.Moves.Count);
                    View.Write(this.output, placed);
                    Console.WriteLine();
                }
                List<int> nextMoves;

                if (next.NextMoves != null) // The next move didn't find any valid values, try different move
                {
                    nextMoves = new List<int>(next.NextMoves);
                }
                else
                {
                    nextMoves = new List<int>(next.Moves);
                }

                List<int> temp = new List<int>();
                bool addToTemp = false;
                for (int i = 0; i < nextMoves.Count; i++)
                {
                    if (rules.CheckValid(next.Y, next.X, nextMoves[i]))
                    {
                        if (addToTemp) // add all possible moves to NextMoves list, incase the next didn't find any valid values
                        {
                            temp.Add(nextMoves[i]);
                        }
                        else
                        {
                            // place the first possible move on grid
                            addToTemp = true;
                            this.output[next.Y, next.X] = nextMoves[i];
                            rules.AddToGrid(next.Y, next.X, nextMoves[i]);
                            
                            if (Program.feedback)
                            {
                                Console.WriteLine("Succesfully placed: " + nextMoves[i]);
                                placed = next.Y + "" + next.X;
                            }
                        }
                    }
                }
                if (!addToTemp) // can't place any value
                {
                    var prev = next.Previous;
                    if (prev != null)
                    {
                        // do previous move undone
                        int value = this.output[prev.Y, prev.X];
                        this.output[prev.Y, prev.X] = 0;
                        rules.RemoveFromGrid(prev.Y, prev.X, value);
                        next.NextMoves = null;
                        placed = null;
                        next = prev;

                        if (Program.feedback)
                        {
                            Console.WriteLine("Failed placing any values! Removing: " + value + " from (" + prev.Y + "" + prev.X + ")");
                        }
                    }
                    else
                    {
                        Console.WriteLine("It is impossible!");
                        return;
                    }
                }
                else // go to next step
                {
                    next.NextMoves = temp;
                    next = next.Next;
                }
                this.steps++;
            }
            Console.WriteLine();
            Console.WriteLine("Solution found!(" + this.steps + ")");
            View.Write(this.output);
        }
    }
}
