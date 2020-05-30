using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Sudoku
{
    class Calculator
    {
        private int[,] input;
        public int[,] output;
        private Rules rules;

        private bool fillup;
        SortedDictionary<string, LinkedMoves> possibleMoves;
        private string[] order;
        private LinkedMoves start;
        private LinkedMoves end;

        public Calculator(int[,] grid)
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
            while (fillup)
            {
                this.CreatePossible();
                this.UpdatePossible();
            }

            this.CreatePossible();

            if (this.possibleMoves.Count == 0)
            {
                Console.WriteLine("Solution found!");
                View.Write(this.output);
                return;
            }

            this.CreateLinkedListStartAtLowestPossibleMoves();
            this.CreateLinkedList();
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

        private void CreateLinkedListStartAtLowestPossibleMoves()
        {
            int moves = 9;
            this.order = new string[this.possibleMoves.Count];
            int i = 0;
            LinkedMoves l = new LinkedMoves(null,null);
            foreach (KeyValuePair<string, LinkedMoves> entry in this.possibleMoves)
            {
                var a = entry.Value.Moves;
                if (a.Count < moves)
                {
                    moves = a.Count;
                    l = entry.Value;
                    this.order[0] = entry.Key;
                    i = 1;
                }
                else
                {
                    this.order[i] = entry.Key;
                    i++;
                } 
            }
            // go through skipped keys
            foreach (KeyValuePair<string, LinkedMoves> entry in this.possibleMoves)
            {
                if (entry.Key.Equals(this.order[0]))
                    break;

                for (int j = 0; j < this.order.Length; j++)
                {
                    if (this.order[j] == null)
                    {
                        this.order[j] = entry.Key;
                        break;
                    }
                }
            }
            this.start = l;
            this.end = this.start;
        }

        private void CreateLinkedList()
        {
           for (int i = 0; i < order.Length; i++)
           {
                if (i != order.Length - 1)
                {
                    // take first value of possible move
                    // check if valid
                    // if valid check if next first value of possible move

                    // if not valid check next value is possible

                    // this.start.Next = this.possibleMoves[this.order[i + 1]];
                    // this.start.Next.Previous = this.start;


                }
           }

            // done
        }

        private void DebugPrintAll()
        {
            Console.WriteLine("=================");
            foreach (KeyValuePair<string, LinkedMoves> entry in this.possibleMoves)
            {
                Console.Write(entry.Key + "--------->");
                this.DebugPrintList(entry.Value.Moves);
            }
            Console.WriteLine("=================");
            Thread.Sleep(1000);
        }
        private void DebugPrintList(List<int> list)
        {
            foreach (int i in list)
            {
                Console.Write(" " + i + " ");
            }
            Console.WriteLine();
        }

        

    }


}
