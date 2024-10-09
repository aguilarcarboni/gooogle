using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

// Interface defining the methods that an inverted index should implement
public interface IInvertedIndex 
{
    void AddDocuments(DocumentCollection collection);
    void SaveToJson(string folderName);
}

public class InvertedIndex : IInvertedIndex
{
    // The main data structure for the inverted index
    // Key: word, Value: set of document IDs containing the word
    private Dictionary<string, HashSet<string>> index = new Dictionary<string, HashSet<string>>();

    public void AddDocuments(DocumentCollection collection)
    {
        foreach (var document in collection.Documents)
        {
            string docId = document.FileName;
            
            foreach (var word in document.Words)
            {
                string cleanWord = CleanWord(word);
                if (!string.IsNullOrWhiteSpace(cleanWord) && !OrganizeWords.StopWords.Contains(cleanWord))
                {
                    if (!index.ContainsKey(cleanWord))
                    {
                        index[cleanWord] = new HashSet<string>();
                    }
                    index[cleanWord].Add(docId);
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
        string invertedIndexDir = "INVERTED-INDEX";
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