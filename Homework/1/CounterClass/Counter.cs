using System;

namespace CounterClass
{
    /// <summary> Counter </summary>
    public class Counter
    {
        /// <summary> id </summary> 
        string id;
        /// <summary> x </summary>
        int x;

        /// <summary> Constructor </summary>
        /// <param name="id">id of the counter</param>
        public Counter(string id)
        {
            /*
                1. Set the id of the counter to the value of the parameter
                2. Set the value of x to 0
            */
            this.id = id;
            x = 0;
        }

        /// <summary> ToString method </summary>
        /// <returns>the id and value of the counter</returns>
        public override string ToString()
        {
            return $"{id}: {x}";
        }
        
        /// <summary> Increment method </summary>
        public void Increment()
        {
            x++;
        }

        /// <summary> GetTally method </summary>
        /// <returns>the value of x</returns>
        public int GetTally()
        {
            return x;
        }
    }
}
