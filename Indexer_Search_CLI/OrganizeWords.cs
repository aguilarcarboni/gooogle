using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;

static class OrganizeWords{
    public static List<string> String_to_List(Dictionary<string, string> documents)
    {
    // List to collect all words from all documents
    
    List<string> allWords = new List<string>{};
    char[] delimiters = { ',', ';', ' ', '.', '!', '?', '\n', '\r', '\t', '<', '>', '(', ')', '[', ']', '{', '}', '/', '\\', '*', '&', '^', '%', '$', '#', '@', '~', '|', '=', '+' };

    // Process each document
    foreach (var document in documents)
    {
        // Split the string using multiple delimiters
        string[] substrings = document.Value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        // Add each substring to the list
        allWords.AddRange(substrings);
    }

    return allWords;
    }

    public static Dictionary<string, List<string>> CreateWordLists(Dictionary<string, string> documents)
    {
    // List to collect lists of words from each document
    Dictionary<string, List<string>> wordListsDictionary = new Dictionary<string, List<string>>();
    char[] delimiters = { ',', ';', ' ', '.', '!', '?', '\n', '\r', '\t', '<', '>', '(', ')', '[', ']', '{', '}', '/', '\\', '*', '&', '^', '%', '$', '#', '@', '~', '|', '=', '+' };

    // Process each document
    foreach (var document in documents)
    {
        // Split the string using multiple delimiters
        string[] substrings = document.Value.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

        // Convert to list of words and add to the list of lists
        List<string> wordList = new List<string>(substrings);

        // Add the file name and word list to the dictionary
        wordListsDictionary[document.Key] = wordList;
    }

    return wordListsDictionary;
    }

    public static List<string> Unique_Words(List<string> documents)
    {
        // Use a HashSet to eliminate words that are repeating
        HashSet<string> uniqueWords = new HashSet<string>();

        // Get all words from the documents
        List<string> allWords = documents;

        // Add each word to the HashSet
        foreach (string word in allWords)
        {
            uniqueWords.Add(word.ToLower()); // Convert to lowercase for case-insensitive comparison
        }

        // Convert HashSet to List
        List<string> myList = new List<string>(uniqueWords);

        // Return the list of unique words
        return myList;
    }

public static Dictionary<string, Dictionary<string, int>> CountWordOccurrences(Dictionary<string, List<string>> docs, List<string> allUniqueWords)
{
    // Initialize a dictionary to store word count dictionaries for each document
    Dictionary<string, Dictionary<string, int>> wordCounts = new Dictionary<string, Dictionary<string, int>>();

    // Iterate through each entry in the docs dictionary
    foreach (var doc in docs)
    {
        string fileName = doc.Key;
        List<string> wordList = doc.Value;

        // Create a new dictionary to hold word counts for the current document
        Dictionary<string, int> wordCount = new Dictionary<string, int>();

        // Initialize the dictionary with all unique words, setting their counts to 0
        foreach (var uniqueWord in allUniqueWords)
        {
            wordCount[uniqueWord] = 0;
        }

        // Count occurrences of words in the current document
        foreach (string word in wordList)
        {
            // Convert the word to lowercase to ensure case-insensitive counting
            string wordLower = word.ToLower();
            
            // If the word is in the dictionary, increment its count
            if (wordCount.ContainsKey(wordLower))
            {
                wordCount[wordLower]++;
            }
        }

        // Add the completed word count dictionary for the current document to the dictionary
        wordCounts[fileName] = wordCount;
    }

    // Return the dictionary of dictionaries, each representing word counts for a different document
    return wordCounts;
}

    public static Dictionary<string, int> ComputeDocumentFrequency(Dictionary<string, Dictionary<string, int>> wordOccurrences)
    {
        // Initialize a dictionary to store the number of documents each word appears in
        Dictionary<string, int> documentFrequency = new Dictionary<string, int>();

        // Iterate through each document's word count dictionary
        foreach (var docEntry in wordOccurrences)
        {
            Dictionary<string, int> wordDict = docEntry.Value;

            // Use a HashSet to ensure we count each word only once per document
            HashSet<string> seenWords = new HashSet<string>();

            // Iterate through each word in the current document's dictionary
            foreach (var wordEntry in wordDict)
            {
                string word = wordEntry.Key;
                int count = wordEntry.Value;

                // Only process the word if its count is greater than 0
                if (count > 0 && !seenWords.Contains(word))
                {
                    // If the word has not been counted for this document
                    if (!seenWords.Contains(word))
                    {
                        // Increment the count for this word in the document frequency dictionary
                        if (documentFrequency.ContainsKey(word))
                        {
                            documentFrequency[word]++;
                        }
                        else
                        {
                            documentFrequency[word] = 1;
                        }

                        // Mark the word as seen for this document
                        seenWords.Add(word);
                    }
                }
            }
        }

        return documentFrequency;
    }
}