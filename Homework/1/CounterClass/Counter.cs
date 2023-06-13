using System;

namespace CounterClass
{
    public class Counter
    {
        string id;
        int x;
        public Counter(string id)
        {
            this.id = id;
            x = 0;
        }

        public override string ToString()
        {
            return $"{id}: {x}";
        }

        public void Increment()
        {
            x++;
        }

        public int GetTally()
        {
            return x;
        }
    }
}
