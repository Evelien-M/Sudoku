using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Algorithm
    {
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
            int steps = 1;
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
                    Console.WriteLine("Step " + steps);
                    View.Write(this.output);
                }
                steps++;
            }

            this.CreatePossible();

            if (this.possibleMoves.Count == 0)
            {
                if (this.IsCompleted())
                {
                    Console.WriteLine("Solution found!");
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
                Console.WriteLine("Setting up order to loop through...");
            }
            this.order = new string[this.possibleMoves.Count];

            var a = this.possibleMoves.OrderBy(o => o.Value.Moves.Count);
            var b = a.Select(s => s.Key).ToArray();
            this.order = b;
        }

        private void CreateLinkedList()
        {
            for (int i = 0; i < order.Length; i++)
            {
                if (Program.feedback)
                        Console.Write(order[i] + "------>");

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
                Console.WriteLine();
        }

        private void LoopThrough()
        {
            var next = this.start;
            
            while (next != null)
            {
                if (Program.feedback)
                {
                    Console.WriteLine("==================================================================");
                    Console.WriteLine("(" + next.Y + "" + next.X + ") Amount of moves: " + next.Moves.Count);
                }
                List<int> nextMoves;

                if (next.NextMoves != null) // next got called by next.next
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
                    if (rules.CheckValid(next.Y, next.X, nextMoves[i])) // is valid go to next move
                    {
                        if (addToTemp)
                        {
                            temp.Add(nextMoves[i]);
                        }
                        else
                        {
                            addToTemp = true;
                            this.output[next.Y, next.X] = nextMoves[i];
                            rules.AddToGrid(next.Y, next.X, nextMoves[i]);
                            
                            if (Program.feedback)
                            {
                                Console.WriteLine("Succesfully placed: " + nextMoves[i]);
                                View.Write(this.output);
                            }
                        }
                    }
                }
                if (!addToTemp) // can't place a value
                {
                    var prev = next.Previous;
                    if (prev != null)
                    {
                        // do move undone
                        int value = this.output[prev.Y, prev.X];
                        this.output[prev.Y, prev.X] = 0;
                        rules.RemoveFromGrid(prev.Y, prev.X, value);
                        next.NextMoves = null;

                        next = prev;

                        if (Program.feedback)
                        {
                            Console.WriteLine("Failed placing any values! Removing: " + value);
                            View.Write(this.output);
                        }
                    }
                    else
                    {
                        Console.WriteLine("It is impossible!");
                        return;
                    }
                }
                else
                {
                    next.NextMoves = temp;
                    next = next.Next;
                } 
            }

            Console.WriteLine("Solution found!");
            View.Write(this.output);
        }
    }
}
