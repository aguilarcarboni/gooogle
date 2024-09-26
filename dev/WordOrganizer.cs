using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;

static class WordOrganizer{

    // Esto deberia estar en document
    public static List<string> StringToList(List<string> documents) {
        // List to collect all words from all documents
        
        List<string> allWords = new List<string>{};
        char[] delimiters = { ',', ';', ' ' };

        // Process each document
        foreach (string document in documents) {
            // Split the string using multiple delimiters
            string[] substrings = document.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Add each substring to the list
            allWords.AddRange(substrings);
        }

        return allWords;
    }

    // Esto deberia estar en document
    public static List<List<string>> CreateWordLists(List<string> documents) {
        // List to collect lists of words from each document
        List<List<string>> listOfWordLists = new List<List<string>>();
        char[] delimiters = { ',', ';', ' ' };

        // Process each document
        foreach (string document in documents)
        {
            // Split the string using multiple delimiters
            string[] substrings = document.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            // Convert to list of words and add to the list of lists
            List<string> wordList = new List<string>(substrings);
            listOfWordLists.Add(wordList); // Add the new list of words to the list of lists
        }

        return listOfWordLists;
    }

    // Esto deberia estar en document o Drive
    public static List<Dictionary<string, int>> CountWordOccurrences(List<List<string>> docs, List<string> allUniqueWords) {
        // Initialize a list to store word count dictionaries for each document
        List<Dictionary<string, int>> wordCounts = new List<Dictionary<string, int>>();

        // Iterate through each list of words (representing a document) in the input list
        foreach (var wordList in docs) {
            // Create a new dictionary to hold word counts for the current document
            Dictionary<string, int> wordCount = new Dictionary<string, int>();

            // Initialize the dictionary with all unique words, setting their counts to 0
            foreach (var uniqueWord in allUniqueWords) {
                wordCount[uniqueWord] = 0;
            }

            // Count occurrences of words in the current document
            foreach (string word in wordList) {
                // Convert the word to lowercase to ensure case-insensitive counting
                string wordLower = word.ToLower();
                
                // If the word is in the dictionary, increment its count
                if (wordCount.ContainsKey(wordLower)) {
                    wordCount[wordLower]++;
                }
            }

            // Add the completed word count dictionary for the current document to the list
            wordCounts.Add(wordCount);
        }

        // Return the list of dictionaries, each representing word counts for a different document
        return wordCounts;
    }

    // Esto deberia estar en otro objeto de Drive
    public static Dictionary<string, int> ComputeDocumentFrequency(List<Dictionary<string, int>> wordOccurrences) {
        // Initialize a dictionary to store the number of documents each word appears in
        Dictionary<string, int> documentFrequency = new Dictionary<string, int>();

        // Iterate through each document's word count dictionary
        foreach (var wordDict in wordOccurrences) {
            // Use a HashSet to ensure we count each word only once per document
            HashSet<string> seenWords = new HashSet<string>();

            // Iterate through each word in the current document's dictionary
            foreach (var wordEntry in wordDict) {
                string word = wordEntry.Key;
                int count = wordEntry.Value;

                // Only process the word if its count is greater than 0
                if (count > 0) {
                    // If the word has not been counted for this document
                    if (!seenWords.Contains(word)) {
                        // Increment the count for this word in the document frequency dictionary
                        if (documentFrequency.ContainsKey(word)) {
                            documentFrequency[word]++;
                        }
                        else {
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