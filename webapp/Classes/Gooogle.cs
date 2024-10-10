using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class Gooogle
{
    private string? loadedIndexPath;
    private string? distanceMetric;
    public Dictionary<string, double>? Similarities { get; private set; }

    public void Index(string folder, string indexer, string distance)
    {
        // Validate arguments
        if (string.IsNullOrEmpty(folder) || string.IsNullOrEmpty(indexer) || string.IsNullOrEmpty(distance))
        {
            Console.WriteLine("Invalid arguments for index command.");
            return;
        }

        if (!Directory.Exists(folder))
        {
            Console.WriteLine($"Error: Folder not found at {folder}");
            return;
        }

        distanceMetric = distance;

        // Process documents
        DocumentProcessor processor = new DocumentProcessor();
        DocumentCollection collection = processor.ProcessDocumentsInFolder(folder);

        // Get word occurrences and document frequency
        var wordOccurrences = collection.GetWordOccurrences();
        var documentFrequency = collection.DocumentFrequency;

        // Create and save the chosen index type
        switch (indexer.ToLower())
        {
            case "tf-idf":
                // Compute TF-IDF
                TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
                var tf = tfIdf.ComputeTF(wordOccurrences);
                var idf = tfIdf.ComputeIDF(wordOccurrences, documentFrequency);
                var tf_idf = tfIdf.ComputeTF_IDF(tf, idf);

                // Save TF-IDF to JSON
                tfIdf.SaveTF_IDFToJson(tf_idf, folder);
                break;

            case "inverted":
                InvertedIndex invertedIndex = new InvertedIndex();
                invertedIndex.AddDocuments(collection);
                invertedIndex.SaveToJson(folder);
                break;

            default:
                Console.WriteLine("Error: Unsupported indexer type. Supported types are 'tf-idf' and 'inverted'.");
                return;
        }

        Console.WriteLine($"Indexing complete for folder: {folder}");
        Console.WriteLine($"Using indexer: {indexer}");
        Console.WriteLine($"Distance metric: {distance}");
    }

    public void Load(string indexPath)
    {
        if (indexPath == null)
        {
            Console.WriteLine("Usage: load -p <index_path>");
            return;
        }

        if (!File.Exists(indexPath))
        {
            Console.WriteLine($"Error: File not found at {indexPath}");
            return;
        }

        loadedIndexPath = indexPath;
        Console.WriteLine($"Loaded index from: {indexPath}");
    }

    public void Search(string query, int k)
    {
        if (loadedIndexPath == null)
        {
            Console.WriteLine("Error: No index loaded. Use the 'load' command first.");
            return;
        }

        Console.WriteLine($"Searching for: {query}");
        Console.WriteLine($"Top {k} results:");
        Console.WriteLine($"Using distance metric: {distanceMetric}");

        if (distanceMetric.ToLower() == "euclidean")
        {
            EuclideanSimilarity euclideanSimilarity = new EuclideanSimilarity(loadedIndexPath);
            Similarities = euclideanSimilarity.CalculateSimilarities(query);
        }
        else
        {
            CosineSimilarity cosineSimilarity = new CosineSimilarity(loadedIndexPath);
            Similarities = cosineSimilarity.CalculateSimilarities(query);
        }

        int count = 0;
        foreach (var pair in Similarities.OrderByDescending(x => x.Value))
        {
            if (count >= k) break;
            Console.WriteLine($"{pair.Key}: {pair.Value:F4}");
            count++;
        }

        if (count == 0)
        {
            Console.WriteLine("No results found.");
        }
    }

    // Add this new method
    public void ResetSimilarities()
    {
        Similarities = null;
    }
}