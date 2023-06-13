using System;
using ListSymbolTable;
using System.IO;

namespace SymbolTableDriver
{
    public class Counter
    {
        public int WordCount;
        public double Frequency;
        public Counter(int x = 0)
        {
            WordCount = x;
            Frequency = 0.0;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            ListSymbolTable<string, Counter> data = new ListSymbolTable<string, Counter>();
            string text = File.ReadAllText(args[0]);
            text = text.ToLower();
            char[] splits = { ' ', '.', ',', '?', ':', '!', '\r', '\n' };
            char[] cleanUp = { '(', ')', '\'', '"' };
            string[] words = text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

            int totalWordCount = words.Length;
            for (int i = 0; i < words.Length; i++)
            {
                string stemmed = words[i].Trim(cleanUp);
                if (data.ContainsKey(stemmed)) { data[stemmed].WordCount++; }
                data[stemmed] = new Counter(1);
                foreach (string word in data)
                {
                    data[word].Frequency = data[word].WordCount / words.Length;
                }
            }
        }
    }
}

            
