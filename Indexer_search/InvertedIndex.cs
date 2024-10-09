using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;
using Porter2Stemmer;

// Interface defining the methods that an inverted index should implement
public interface IInvertedIndex 
{
    void AddDocuments(Dictionary<string, List<string>> wordLists);
    void SaveToJson(string folderName);
}

public class InvertedIndex : IInvertedIndex
{
    // The main data structure for the inverted index
    // Key: stemmed word, Value: set of document IDs containing the word
    private Dictionary<string, HashSet<string>> index = new Dictionary<string, HashSet<string>>();
    private EnglishPorter2Stemmer stemmer = new EnglishPorter2Stemmer();

    public void AddDocuments(Dictionary<string, List<string>> wordLists)
    {
        // Get unique words from all documents
        List<string> allWords = wordLists.Values.SelectMany(list => list).ToList();
        List<string> uniqueWords = OrganizeWords.Unique_Words(new List<string> { string.Join(" ", allWords) });

        // Iterate through each document in the word lists
        foreach (var doc in wordLists)
        {
            string docId = doc.Key;
            List<string> words = doc.Value;

            // Process each word in the document
            foreach (var word in words)
            {
                string cleanWord = CleanWord(word);
                // Only add non-empty words that are not stop words
                if (!string.IsNullOrWhiteSpace(cleanWord) && !OrganizeWords.StopWords.Contains(cleanWord))
                {
                    string stemmedWord = stemmer.Stem(cleanWord).Value;
                    // If the stemmed word is in our unique words list
                    if (uniqueWords.Contains(stemmedWord))
                    {
                        // If the word is not in the index, add it with a new HashSet
                        if (!index.ContainsKey(stemmedWord))
                        {
                            index[stemmedWord] = new HashSet<string>();
                        }
                        // Add the document ID to the set of documents containing this word
                        index[stemmedWord].Add(docId);
                    }
                }
            }
        }
    }

    // Helper method to clean and normalize words
    private string CleanWord(string word)
    {
        return word.Trim().ToLower();
    }

    public void SaveToJson(string folderName)
    {
        // Ensure the InvertedIndex directory exists
        string invertedIndexDir = "InvertedIndex";
        Directory.CreateDirectory(invertedIndexDir);

        // Create the file name based on the folder name
        string fileName = $"{folderName}_inverted_index.json";

        // Create the full file path
        string filePath = Path.Combine(invertedIndexDir, fileName);

        // Convert HashSet to List for JSON serialization
        var serializableIndex = index.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.ToList()
        );

        // Serialize the index to JSON
        string jsonString = JsonSerializer.Serialize(serializableIndex, new JsonSerializerOptions
        {
            WriteIndented = true, // This makes the JSON file more readable
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping, // This allows more characters without escaping
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // Write the JSON string to the file
        File.WriteAllText(filePath, jsonString);

        Console.WriteLine($"Inverted index for {folderName} saved to: {filePath}");
    }
}