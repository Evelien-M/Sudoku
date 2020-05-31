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

        
        public LinkedMoves(string key, List<int> moves)
        {
            if (key != null && moves != null)
            {
                this.Moves = moves;
                this.Y = Int32.Parse(key[0].ToString());
                this.X = Int32.Parse(key[1].ToString());
            }
        }
    }
}
