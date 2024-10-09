using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

static class CLI
{
    // Store the path of the loaded index and the chosen distance metric
    private static string? loadedIndexPath = null;
    private static string? distanceMetric = null;

    static void Main()
    {
        Console.WriteLine("Welcome to the Indexer Search");
        // Main loop for the CLI
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? "";
            string[] args = input.Split(' ');

            if (args.Length == 0) continue;

            // Process user commands
            switch (args[0].ToLower())
            {
                case "index":
                    HandleIndex(args);
                    break;
                case "load":
                    HandleLoad(args);
                    break;
                case "search":
                    HandleSearch(args);
                    break;
                case "exit":
                    return;
                default:
                    Console.WriteLine("Invalid command. Available commands: index, load, search, exit");
                    break;
            }
        }
    }

    static void HandleIndex(string[] args)
    {
        // Check if the correct number of arguments is provided
        if (args.Length != 7)
        {
            Console.WriteLine("Usage: index -f <folder> -t <indexer> -dis <distance>");
            return;
        }

        // Parse command arguments
        string folder = "";
        string indexer = "";
        string distance = "";

        for (int i = 1; i < args.Length; i += 2)
        {
            switch (args[i])
            {
                case "-f":
                    folder = args[i + 1];
                    break;
                case "-t":
                    indexer = args[i + 1];
                    break;
                case "-dis":
                    distance = args[i + 1];
                    break;
            }
        }

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
        var documents = processor.ProcessDocumentsInFolder(folder);

        Console.WriteLine("Documents processed");

        // Organize words
        var wordLists = OrganizeWords.CreateWordLists(documents);
        var uniqueWords = OrganizeWords.Unique_Words(OrganizeWords.String_to_List(documents));
        var wordOccurrences = OrganizeWords.CountWordOccurrences(wordLists, uniqueWords);
        var documentFrequency = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);

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

            case "inverted-index":

                // Create and populate Inverted Index
                InvertedIndex invertedIndex = new InvertedIndex();
                invertedIndex.AddDocuments(wordLists);

                // Save Inverted Index to JSON
                invertedIndex.SaveToJson(folder);
                break;

            default:
                Console.WriteLine("Error: Unsupported indexer type. Supported types are 'tf-idf' and 'inverted-index'.");
                return;
        }

        Console.WriteLine($"Indexing complete for folder: {folder}");
        Console.WriteLine($"Using indexer: {indexer}");
        Console.WriteLine($"Distance metric: {distance}");
        
    }

    static void HandleLoad(string[] args)
    {
        // Check if the correct number of arguments is provided
        if (args.Length != 3 || args[1] != "-p")
        {
            Console.WriteLine("Usage: load -p <index_path>");
            return;
        }

        string indexPath = args[2];
        if (!File.Exists(indexPath))
        {
            Console.WriteLine($"Error: File not found at {indexPath}");
            return;
        }

        loadedIndexPath = indexPath;
        Console.WriteLine($"Loaded index from: {indexPath}");
    }

    static void HandleSearch(string[] args)
    {
        // Check if the command has the correct format: search -query -k
        if (args.Length != 3)
        {
            Console.WriteLine("Usage: search -<query> -<k>");
            return;
        }

        // Verify that an index has been loaded before attempting to search
        if (loadedIndexPath == null)
        {
            Console.WriteLine("Error: No index loaded. Use the 'load' command first.");
            return;
        }

        // Extract the search query by removing the leading hyphen
        string query = args[1].Substring(1);

        // Parse the 'k' value (number of results to return)
        if (!int.TryParse(args[2].Substring(1), out int k))
        {
            Console.WriteLine("Error: Invalid value for k. Must be an integer.");
            return;
        }

        // Display information about the search
        Console.WriteLine($"Searching for: {query}");
        Console.WriteLine($"Top {k} results:");
        Console.WriteLine($"Using distance metric: {distanceMetric}");

        // Implement cosine similarity search
        CosineSimilarity cosineSimilarity = new CosineSimilarity(loadedIndexPath);
        Dictionary<string, double> similarities = cosineSimilarity.CalculateSimilarities(query);

        // Display the top k results
        int count = 0;
        foreach (var pair in similarities)
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

}
