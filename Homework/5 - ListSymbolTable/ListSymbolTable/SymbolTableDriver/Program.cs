using System;
using ListSymbolTable;
using System.IO;

namespace SymbolTableDriver
{
    /// <summary>
    /// Counter class to keep track of word count and frequency.
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// The number of words counted.
        /// </summary>
        public int WordCount;

        /// <summary>
        /// The frequency of the word count.
        /// </summary>
        public double Frequency;

        /// <summary>
        /// Constructor for the Counter class.
        /// </summary>
        /// <param name="x">The initial word count.</param>
        public Counter(int x = 0)
        {
            WordCount = x;
            Frequency = 0.0;
        }
    }

    /// <summary>
    /// The main program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        static void Main(string[] args)
        {
            // Create a new symbol table to store the word counts.
            ListSymbolTable<string, Counter> data = new ListSymbolTable<string, Counter>();

            // Read the text from the file specified in the command line arguments.
            string text = File.ReadAllText(args[0]);

            // Convert the text to lowercase.
            text = text.ToLower();

            // Define the characters to split the text on.
            char[] splits = { ' ', '.', ',', '?', ':', '!', '\r', '\n' };

            // Define the characters to clean up from the words.
            char[] cleanUp = { '(', ')', '\'', '"' };

            // Split the text into words.
            string[] words = text.Split(splits, StringSplitOptions.RemoveEmptyEntries);

            // Count the total number of words.
            int totalWordCount = words.Length;

            // Loop through each word and update the word count and frequency.
            for (int i = 0; i < words.Length; i++)
            {
                // Remove any unwanted characters from the word.
                string stemmed = words[i].Trim(cleanUp);

                // If the word is already in the symbol table, increment its count.
                if (data.ContainsKey(stemmed))
                {
                    data[stemmed].WordCount++;
                }

                // Otherwise, add the word to the symbol table with a count of 1.
                data[stemmed] = new Counter(1);

                // Update the frequency of each word in the symbol table.
                foreach (string word in data)
                {
                    data[word].Frequency = data[word].WordCount / words.Length;
                }
            }
        }
    }
}


