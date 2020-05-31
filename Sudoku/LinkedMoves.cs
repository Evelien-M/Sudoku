using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku
{
    public class LinkedMoves
    {
        public LinkedMoves Next { get; set; }
        public LinkedMoves Previous { get; set; }
        public List<int> Moves { get; private set; }

        public List<int> NextMoves { get; set; }
        public int Y { get; private set; }
        public int X { get; private set; }

        private int amount;
        
        public LinkedMoves(string key, List<int> moves)
        {
            if (key != null && moves != null)
            {
                this.Moves = moves;
                this.Y = Int32.Parse(key[0].ToString());
                this.X = Int32.Parse(key[1].ToString());
            }
        }

        public bool ToNextMove()
        {
            this.amount++;

            this.Order();
            if (this.amount == this.Moves.Count)
            {
                this.amount = 0;
                return false;
            }

            return true;
        }

        private void Order()
        {
            int end = Moves[0];

            for (int i = 0; i < Moves.Count; i++)
            {
                this.Swap(i, i + 1);
            }
            Moves[Moves.Count - 1] = end;
        }
        private void Swap(int indexA, int indexB)
        {
            if (Moves.Count <= indexA || Moves.Count <= indexB)
                return;

            var tmp = Moves[indexA];
            Moves[indexA] = Moves[indexB];
            Moves[indexB] = tmp;
        }
    }
}
