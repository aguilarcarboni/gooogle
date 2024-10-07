using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;
using System.IO;
using System.Text.Json;

static class TF_IDF{

    public interface ITF_IDF
    {
        Dictionary<string, Dictionary<string, double>> ComputeTF(Dictionary<string, Dictionary<string, int>> WordOccurrences);
        Dictionary<string, double> ComputeIDF(Dictionary<string, Dictionary<string, int>> wordOccurrences, Dictionary<string, int> documentFrequency);
        Dictionary<string, Dictionary<string, double>> ComputeTF_IDF(Dictionary<string, Dictionary<string, double>> tf, Dictionary<string, double> idf);
        void SaveTF_IDFToJson(Dictionary<string, Dictionary<string, double>> tf_idf, string sourceFolder);
    }

    public class tf_idf : ITF_IDF{
        public Dictionary<string, Dictionary<string, double>> ComputeTF(Dictionary<string, Dictionary<string, int>> WordOccurrences)
    {
        // Initialize a dictionary to hold TF dictionaries for each document
        Dictionary<string, Dictionary<string, double>> tfDictionary = new Dictionary<string, Dictionary<string, double>>();

        foreach (var docEntry in WordOccurrences)
        {
            string fileName = docEntry.Key;//Gets the file name
            Dictionary<string, int> wordDict = docEntry.Value;//Gets the word dictionary from that file

            // Initialize a new dictionary to hold TF values for the current document
            Dictionary<string, double> tfDict = new Dictionary<string, double>();

            // Calculate the total number of words in the document
            int totalWords = wordDict.Values.Sum();

            // Calculate TF for each word in the document
            foreach (var wordEntry in wordDict)
            {
                string word = wordEntry.Key;
                int count = wordEntry.Value;
                double tf = (double)count / totalWords;
                tfDict[word] = tf;
            }

            // Add the TF dictionary for the current document to the main dictionary
            tfDictionary[fileName] = tfDict;
        }

        return tfDictionary;
    }

    public Dictionary<string, double> ComputeIDF(Dictionary<string, Dictionary<string, int>> wordOccurrences, Dictionary<string, int> documentFrequency)
    {
        // Initialize a dictionary to store the IDF values for each word
        Dictionary<string, double> idfList = new Dictionary<string, double>();

        // Get the number of documents
        int numberOfDocuments = wordOccurrences.Count;

        // Calculate the IDF for each word
        foreach (var entry in documentFrequency)
        {
            string word = entry.Key;
            int docFrequency = entry.Value;

            // Compute the IDF value using the formula
            double idf = Math.Log((double)numberOfDocuments / docFrequency);

            // Add the IDF value to the dictionary
            idfList[word] = idf;
        }

        return idfList;
    }

    public Dictionary<string, Dictionary<string, double>> ComputeTF_IDF(Dictionary<string, Dictionary<string, double>> tf, Dictionary<string, double> idf)
    {
        // Initialize a dictionary to hold TF-IDF dictionaries for each document
        Dictionary<string, Dictionary<string, double>> tf_idf = new Dictionary<string, Dictionary<string, double>>();

        // Get all unique words from the IDF dictionary (global vocabulary)
        HashSet<string> allWords = new HashSet<string>(idf.Keys);

        // Iterate through each TF dictionary (one for each document)
        foreach (var docEntry in tf)
        {
            string fileName = docEntry.Key;
            Dictionary<string, double> tfDict = docEntry.Value;

            // Initialize a new dictionary to hold TF-IDF values for the current file
            Dictionary<string, double> tf_idf_Dict = new Dictionary<string, double>();

            // Compute TF-IDF for each word in the global vocabulary
            foreach (string word in allWords)
            {
                // If the word is not in the current document, use TF = 0
                double tfValue = tfDict.TryGetValue(word, out double value) ? value : 0.0;
                
                // The word should always be in the IDF dictionary
                if (idf.TryGetValue(word, out double idfValue))
                {
                    // Compute TF-IDF and add it to the dictionary
                    double tf_idf_value = tfValue * idfValue;
                    tf_idf_Dict[word] = tf_idf_value;
                }
            }

            // Add the TF-IDF dictionary for the current document to the main dictionary
            tf_idf[fileName] = tf_idf_Dict;
        }

        return tf_idf;
    }

    public void SaveTF_IDFToJson(Dictionary<string, Dictionary<string, double>> tf_idf, string sourceFolder)
    {
        // Ensure the TF-IDF folder exists
        Directory.CreateDirectory("TF_IDF");

        // Create the file name based on the source folder
        string fileName = $"{Path.GetFileName(sourceFolder)}_tf_idf.json";

        // Create the full file path
        string filePath = Path.Combine("TF_IDF", fileName);

        // Serialize the tf_idf dictionary to JSON
        string jsonString = JsonSerializer.Serialize(tf_idf, new JsonSerializerOptions
        {
            WriteIndented = true // This makes the JSON file more readable
        });

        // Write the JSON string to the file
        File.WriteAllText(filePath, jsonString);

        Console.WriteLine($"TF-IDF results for {sourceFolder} saved to: {filePath}");
    }
    }
    
}