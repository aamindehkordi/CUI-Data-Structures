using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using SymbolTableLibrary;

public class Counter
{
    public int WordCount;
    public double Frequency;

    public Counter(int wordCount = 0, double frequency = 0)
    {
        this.WordCount = wordCount;
        this.Frequency = frequency;
    }

    public override string ToString()
    {
        return $"{WordCount}/{Frequency:N2}";
    }
}

public class WordCounter
{
    public string Word;
    public Counter Stats;

    public WordCounter(string word)
    {
        this.Word = word;
        this.Stats = new Counter();
    }

    public override string ToString()
    {
        return $"{Word}: {Stats}";
    }
}

class WordCountReader
{
    public enum AdtType
    {
        TreeAdt,
        ListAdt,
    }

    public enum SortType
    {
        SelectionSort,
        MergeSort,
    }

    public enum RankType
    {
        RankRawCount,
        RankNormalized,
    }

    // Every word in the entire document, its total count, and relative frequency
    private SymbolTable<string, Counter> masterWordList;
    private int documentWordCount;
    public int UniqueWordCount { get { return masterWordList.Count; } }
    public int DocumentWordCount { get { return documentWordCount; } }

    // Only those words that fall between the low/high cutoff frequencies,
    // sorted in ascending order [0..n]
    private List<WordCounter> filteredWordList;
    public int FilteredWordCount { get { return filteredWordList.Count; } }

    // Tracks the performance of tree vs list symbols counters and the various
    // sorting algorithms
    private Stopwatch timerSymbolTable;
    private Stopwatch timerSortingCode;
    public long LoadTime { get { return timerSymbolTable.ElapsedMilliseconds; } }
    public long SortTime { get { return timerSortingCode.ElapsedMilliseconds; } }

    public WordCountReader(string file, AdtType type = AdtType.TreeAdt)
    {
        // Create the master word list for this document depending on which
        // type of symbol table the user wants us to use
        if (type == AdtType.ListAdt)
        {
            masterWordList = new ListSymbolTable<string, Counter>();
        }
        else
        {
            masterWordList = new TreeSymbolTable<string, Counter>();
        }

        // Initialize all of the other class properties
        filteredWordList = new List<WordCounter>();
        timerSymbolTable = new Stopwatch();
        timerSortingCode = new Stopwatch();
        documentWordCount = 0;

        SymbolTable<string, Counter> mWL = masterWordList;
        string text = File.ReadAllText(file); timerSymbolTable.Restart();
        char[] splits = { ' ', '.', ',', '?', ':', '!', '\r', '\n', '&', ';' };
        char[] trim = { '(', ')', '\'', '"' }; string stemmedWord;
        string[] words = text.Split(splits, StringSplitOptions.RemoveEmptyEntries);
        documentWordCount = words.Length;
        for (int i = 0; i < documentWordCount; i++)
        {
            stemmedWord = words[i].Trim(trim).ToLower();
            if (mWL.ContainsKey(stemmedWord)) { mWL[stemmedWord].WordCount++; }
            else
            {
                mWL.Add(stemmedWord, new Counter(1));
            }

        }
        foreach (string word in mWL)
        {////////////////////////////////////////////////////////////////////////////
            mWL[word].Frequency = (100.0 * mWL[word].WordCount) / documentWordCount;
        }////////////////////////////////////////////////////////////////////////////
        timerSymbolTable.Stop();
    }

    /// <summary>
    /// Sorts a list of words using the selection sort algorithm
    /// </summary>
    /// <param name="words">Lis of words to sort</param>
    private void SelectionSort(List<WordCounter> words)
    {
        for (int i = 0; i < words.Count; i++)
        {
            int max = words[i].Stats.WordCount;
            int idx = i;
            for (int j = i + 1; j < words.Count; j++)
            {
                if (words[j].Stats.WordCount > max) { max = words[j].Stats.WordCount; idx = j; }
            }
            WordCounter temp = words[i];
            words[i] = words[idx];
            words[idx] = temp;
        }
    }

    /// <summary>
    /// Sorts a list of words using the MergeSort algorithm
    /// </summary>
    /// <param name="words">List of words to sort</param>
    private void MergeSort(List<WordCounter> wordd)
    {
        wordd.Sort();
        /*WordCounter[] words = wordd.ToArray();
        MergeSort(words, 0, words.Length - 1);*/
    }
    private void MergeSort(WordCounter[] words, int A, int B)
    {
        int count = B - A + 1;
        if (count <= 1)
        {
            return;
        }

        int M = A + (B - A) / 2;

        MergeSort(words, A, M);
        MergeSort(words, M + 1, B);

        MergeLists(words, A, M, B);
    }
    private void MergeLists(WordCounter[] words, int A, int M, int B)
    {
        List<WordCounter> tArr = new List<WordCounter>(B - A + 1);
        int L = A;
        int R = M + 1;
        int T = 0;

        while (L <= M && R <= B)
        {
            if (words[L].Stats.WordCount < words[R].Stats.WordCount)
            {
                tArr[T++] = words[L++];
            }
            else { tArr[T++] = words[R++]; }
        }

        for (int i = 0; i <= T; i++)
        {
            Console.Write(tArr[i]);
        }
        Console.WriteLine();
    }

    /// <summary>
    /// Returns the filtered word list as a SymbolTable to be used for scoring
    /// </summary>
    public TreeSymbolTable<string, Counter> Words
    {
        get
        {
            // If the filtered word list does not exist yet, create it
            if (filteredWordList.Count == 0 && masterWordList.Count > 0)
            {
                FilterWords(1, 0, SortType.MergeSort);
            }

            // Convert from the filtered word list back to a symbol table
            TreeSymbolTable<string, Counter> words;
            words = new TreeSymbolTable<string, Counter>();
            foreach (WordCounter wc in filteredWordList)
            {
                words.Add(wc.Word, wc.Stats);
            }

            return words;
        }
    }

    /// <summary>
    /// Create a filtered and sorted list of words in this document
    /// </summary>
    /// <param name="ceiling">Highest frequency to allow</param>
    /// <param name="floor">Lowest frequency to allow</param>
    /// <param name="sort">Choice of sort algorithm</param>
    public void FilterWords(double ceiling = 1.00, double floor = 0.01, SortType sort = SortType.SelectionSort)
    {
        filteredWordList = new List<WordCounter>();
        foreach (string w in masterWordList)
        {
            double freq = masterWordList[w].Frequency;
            if (freq < ceiling && freq >= floor)
            {
                WordCounter wc = new WordCounter(w);
                wc.Stats.WordCount = masterWordList[w].WordCount;
                wc.Stats.Frequency = masterWordList[w].Frequency;
                filteredWordList.Add(wc);
            }
        }
        if (sort == SortType.SelectionSort)
        {
            timerSortingCode.Restart();
            SelectionSort(filteredWordList);
        }
        else
        {
            MergeSort(filteredWordList);
        }
        timerSortingCode.Stop();
    }

    /// <summary>
    /// Prints the most common and the least common words from the document, after the filter has been applied.
    /// </summary>
    /// <param name="n">Total number of words to print</param>
    /// <param name="type">Print the total count or the relative frequency of each word</param>
    public void DumpWords(int n = 100, RankType type = RankType.RankNormalized)
    {
        n = Math.Min(n, filteredWordList.Count);
        int topHalf = (int)Math.Ceiling((double)n / 2);
        int botHalf = n - topHalf;

        if (filteredWordList.Count > 0)
        {
            DumpTopWords(topHalf, type);
            Console.WriteLine("...");
            DumpLowWords(botHalf, type);
        }
    }

    private void DumpTopWords(int n = 100, RankType type = RankType.RankNormalized)
    {
        if (filteredWordList.Count > 0)
        {
            int endIdx = Math.Min(n, filteredWordList.Count);
            string format = WordRankFormatString(type, filteredWordList[0]);
            for (int rank = 0; rank < endIdx; rank++)
            {
                Console.WriteLine(WordRankString(type, format, filteredWordList[rank]));
            }
        }
    }

    private void DumpLowWords(int n = 100, RankType type = RankType.RankNormalized)
    {
        if (filteredWordList.Count > 0)
        {
            int startIdx = Math.Max(0, filteredWordList.Count - n);
            string format = WordRankFormatString(type, filteredWordList[startIdx]);
            for (int rank = startIdx; rank < filteredWordList.Count; rank++)
            {
                Console.WriteLine(WordRankString(type, format, filteredWordList[rank]));
            }
        }
    }

    private string WordRankFormatString(RankType type, WordCounter sample)
    {
        int maxWidth;
        string format;
        if (type == RankType.RankNormalized)
        {
            maxWidth = $"{(100.0 * sample.Stats.Frequency / documentWordCount):N2}".Length;
            format = "{0," + maxWidth + ":N2} {1}";
        }
        else
        {
            maxWidth = sample.Stats.WordCount.ToString().Length;
            format = "{0," + maxWidth + "} {1}";
        }

        return format;
    }

    private string WordRankString(RankType type, string format, WordCounter sample)
    {
        double weight;

        if (type == RankType.RankNormalized)
        {
            weight = 100.0 * sample.Stats.Frequency / documentWordCount;
        }
        else
        {
            weight = sample.Stats.WordCount;
        }

        return string.Format(format, weight, sample.Word);
    }
}

class Program
{
    // Prints the usage statement for the program
    static void WriteUsageStatement()
    {
        Console.WriteLine($"Syntax: " +
                          $"{System.AppDomain.CurrentDomain.FriendlyName} " +
                          $"<textfile1> <textfile2> " +
                          $"[-l or -t] " +
                          $"[-s or -m] " +
                          $"[-c <value>]" +
                          $"[-f <value>]" +
                          $"[-v]");
        Console.WriteLine();
        Console.WriteLine($"Default configuration uses binary tree, merge sort, and 0.01-1 cutoff");
        Console.WriteLine();
        Console.WriteLine($"To change settings, use:");
        Console.WriteLine($"  -l: symbol table based on linked list ADT");
        Console.WriteLine($"  -t: symbol table based on binary tree ADT");
        Console.WriteLine($"  -s: selection sort algorithm");
        Console.WriteLine($"  -m: merge sort algorithm");
        Console.WriteLine($"  -c: cutoff frequency for scoring (highest value)");
        Console.WriteLine($"  -f: cutoff frequency for scoring (lowest value)");
        Console.WriteLine($"  -v: verbose output");
    }

    static void Main(string[] args)
    {
        /* Error check the command line arguments */

        if (args.Length < 2)
        {
            Console.WriteLine($"Error:  Not enough arguments specified");
            WriteUsageStatement();
            Console.WriteLine();
            return;
        }

        string file1 = Path.GetFullPath(args[0]);
        if (!File.Exists(file1))
        {
            Console.WriteLine($"Error:  The file '{file1}' does not exist");
            WriteUsageStatement();
            Console.WriteLine();
            return;
        }

        string file2 = Path.GetFullPath(args[1]);
        if (!File.Exists(file2))
        {
            Console.WriteLine($"Error:  The file '{file2}' does not exist");
            WriteUsageStatement();
            Console.WriteLine();
            return;
        }

        /* Set default options */

        WordCountReader.SortType sort = WordCountReader.SortType.MergeSort;
        WordCountReader.AdtType adt = WordCountReader.AdtType.TreeAdt;
        bool verbose = false;
        double cutoff_high = 1;
        double cutoff_low = 0.01;

        /* Allow the user to override the defaults */

        for (int i = 2; i < args.Length; i++)
        {
            switch (args[i].ToLower())
            {
                case "-l":
                    adt = WordCountReader.AdtType.ListAdt;
                    break;
                case "-t":
                    adt = WordCountReader.AdtType.TreeAdt;
                    break;
                case "-s":
                    sort = WordCountReader.SortType.SelectionSort;
                    break;
                case "-m":
                    sort = WordCountReader.SortType.MergeSort;
                    break;
                case "-c":
                    if (!double.TryParse(args[++i], out cutoff_high) ||
                        cutoff_high > 100 || cutoff_high < 0)
                    {
                        Console.WriteLine($"{cutoff_high}");
                        Console.WriteLine($"Error:  '{args[i]}' is not a valid " +
                                          $"cutoff frequency");
                        WriteUsageStatement();
                        Console.WriteLine();
                        return;
                    }
                    break;
                case "-f":
                    if (!double.TryParse(args[++i], out cutoff_low) ||
                        cutoff_low > 100 || cutoff_low < 0)
                    {
                        Console.WriteLine($"{cutoff_high}");
                        Console.WriteLine($"Error:  '{args[i]}' is not a valid " +
                                          $"cutoff frequency");
                        WriteUsageStatement();
                        Console.WriteLine();
                        return;
                    }
                    break;
                case "-v":
                    verbose = true;
                    break;
            }
        }

        /* Read and filter the two files, displaying performance statistics */

        WordCountReader wc1 = new WordCountReader(file1, adt);
        wc1.FilterWords(cutoff_high, cutoff_low, sort);
        Console.WriteLine($"-= {file1} =-");
        Console.WriteLine($"Parsing time: {wc1.LoadTime} ms");
        Console.WriteLine($"Sorting time: {wc1.SortTime} ms");
        Console.WriteLine($"Word count:   {wc1.UniqueWordCount}");
        Console.WriteLine($"Filter range: {cutoff_low} - {cutoff_high}%");
        Console.WriteLine($"Filtered:     {wc1.FilteredWordCount}");
        Console.WriteLine();
        if (verbose && wc1.FilteredWordCount > 0)
        {
            wc1.DumpWords(20, WordCountReader.RankType.RankRawCount);
            Console.WriteLine();
        }

        WordCountReader wc2 = new WordCountReader(file2, adt);
        wc2.FilterWords(cutoff_high, cutoff_low, sort);
        Console.WriteLine($"-= {file2} =-");
        Console.WriteLine($"Parsing time: {wc2.LoadTime} ms");
        Console.WriteLine($"Sorting time: {wc2.SortTime} ms");
        Console.WriteLine($"Word count:   {wc2.UniqueWordCount}");
        Console.WriteLine($"Filter range: {cutoff_low} - {cutoff_high}%");
        Console.WriteLine($"Filtered:     {wc2.FilteredWordCount}");
        Console.WriteLine();
        if (verbose && wc2.FilteredWordCount > 0)
        {
            wc2.DumpWords(20, WordCountReader.RankType.RankRawCount);
            Console.WriteLine();
        }

        /* Output the final score */

        Console.WriteLine("-= Score =-");
        double score = ScoreDocuments(wc1.Words, wc2.Words);
        Console.WriteLine($"Variation Score={score:N2}");
        Console.WriteLine();
    }

    // Calculates the variation score between two documents
    static double ScoreDocuments(SymbolTable<string, Counter> doc1,
                                 SymbolTable<string, Counter> doc2)
    {
        double score = 0;
        int count = 0;

        foreach (string word in doc1)
        {
            if (doc2.ContainsKey(word))
            {
                score += Math.Pow((Math.Abs(doc1[word].Frequency - doc2[word].Frequency)), 2);
                count++;
            }
        }
        foreach (string word in doc2)
        {
            if (doc1.ContainsKey(word))
            {
                score += Math.Pow((Math.Abs(doc2[word].Frequency - doc1[word].Frequency)), 2);
                count++;
            }
        }

        Console.WriteLine($"Found {count} words in common");
        Console.WriteLine($"Doc1 has {doc1.Count - count} words not in Doc2");
        Console.WriteLine($"Doc2 has {doc2.Count - count} words not in Doc1");
        return score;
    }
}
