using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Sudoku
{
    public class Tile
    {
        private Random rng;
        public bool failed;
        private int amount;
        private int called;
        private int attemps;
        private int next;
        private Counter last;
        public static int test;
        Dictionary<string, Counter> inputs;
        public Tile()
        {
            this.rng = new Random();
            this.amount = 0; // possible inputs;
            this.called = 0;
            this.attemps = 1;
            this.failed = false;
            this.inputs = new Dictionary<string, Counter>();
        }

        public void UpdateAmount()
        {
            this.amount = amount * amount;
        }

        private void PrintAll()
        {
            Console.WriteLine("=================");
            foreach (KeyValuePair<string, Counter> entry in inputs)
            {
                Console.Write(entry.Key + "--------->");
                this.PrintList(entry.Value.List);
            }
            Console.WriteLine("=================");
            Thread.Sleep(1000);
        }
        private void PrintList(List<int> list)
        {
            foreach (int i in list)
            {
                Console.Write(" " + i + " ");
            }
            Console.WriteLine();
        }


        public void Next()
        {
       //     this.PrintAll();
            this.called++;
            if (called == amount)
            {
                failed = false;
            }


            if (called == (amount / 10) * attemps)
            {
            //    Console.WriteLine("Progress: "+ attemps + " out of 10...");
                attemps++;
            }
            this.UpdateInputs();
        }

        private void UpdateInputs()
        {
            foreach (KeyValuePair<string, Counter> entry in inputs) // count up
            {
                if (entry.Value.Add())
                {
                    continue;
                }
                return;
            }
            Console.WriteLine("Finished!");
            this.failed = true;
        }





        public void Add(int y, int x, int value)
        {
            amount++;
            string key = y + "" + x;
            if (this.inputs.ContainsKey(key))
            {
                var a = this.inputs[key];
                a.List.Add(value);
            }
            else
            {
                var l = new List<int>();
                l.Add(value);
                var c = new Counter(l);
                this.inputs.Add(key, c);
            }
        }

        public List<int> Get(int y, int x)
        {
            string key = y + "" + x;

            return this.inputs[key].List;
        }

        public List<int> GetRandom(int y, int x)
        {
            string key = y + "" + x;
            var list = this.inputs[key].List;
            this.Shuffle(list);
            return list;
        }

        public void Shuffle<T>(List<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        /*  private void Update()
          {
              if (attemps == 9)
              {
                  this.failed = true;
              }
              this.Print();
              int end = inputs[0];

              for (int i = 0; i < inputs.Length; i++)
              {
                  this.Swap(i, i + 1);
              }
              inputs[8] = end;

              attemps++;

          }

          private void Print()
          {
              Console.WriteLine();
              for (int i = 0; i < inputs.Length; i++)
              {
                  Console.Write(" "+inputs[i]+" ");
              }
          }

          private void Swap(int i, int j)
          {
              if (inputs.Length <= i || inputs.Length <= j)
                  return;

              var temp = inputs[i];
              inputs[i] = inputs[j];
              inputs[j] = temp;

          }*/

        public class Counter
        {
            public List<int> List { get; private set; }
            public int Length => List.Count;
            public int Amount { get; private set; }
            public Counter(List<int> list)
            {
                this.List = list;
            }

            public bool Add()
            {
                if (List.Count == 1)
                    return true;
/*                if (List.Count == 2)
                {
                    this.Shuffle();
                }
                else
                {
                    var rng = new Random();
                    rng.Shuffle(this.List);
                }*/

                this.Amount++;

                this.Shuffle();
                if (this.Amount == this.Length)
                {
                    this.Amount = 0;
                    return true;
                }

                return false;
            }

            private void Shuffle()
            {
                Tile.test++;
                int end = List[0];

                for (int i = 0; i < List.Count; i++)
                {
                    this.Swap(List, i, i + 1);
                }
                List[List.Count - 1] = end;
            }
            public void Swap<T>(IList<T> list, int indexA, int indexB)
            {
                if (list.Count <= indexA || list.Count <= indexB)
                    return;

                T tmp = list[indexA];
                list[indexA] = list[indexB];
                list[indexB] = tmp;
            }

        }

    }
}