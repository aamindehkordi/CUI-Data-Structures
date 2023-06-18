## ListSymbolTable

This subdirectory contains a 'SymbolTableDriver' subdirectory.

### SymbolTableDriver

This subdirectory contains a `Program.cs` file which demonstrates the usage of a symbol table implemented as a list. The program reads a text file, splits the text into words, and counts the frequency of each word using the symbol table. This is done by creating a new entry in the symbol table for each unique word, and incrementing the count of the entry each time the word is encountered again. The program then prints out the words and their frequencies in descending order of frequency.

The program also provides a `Counter` class to keep track of word count and frequency. The `Counter` class has two properties: `Word`, which holds the word, and `Count`, which holds the count of the word. It also overrides the `ToString` method to return a string representation of the `Counter` object in the format 'Word: Count'.
