using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

static class CLI
{
    private static string? loadedIndexPath = null;
    private static string? distanceMetric = null;

    static void Main()
    {
        Console.WriteLine("Welcome to the Indexer Search");
        while (true)
        {
            Console.Write("> ");
            string input = Console.ReadLine() ?? "";
            string[] args = input.Split(' ');

            if (args.Length == 0) continue;

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
        if (args.Length != 7)
        {
            Console.WriteLine("Usage: index -f <folder> -t <indexer> -dis <distance>");
            return;
        }

        string folder = "";
        string indexer = "";
        string distance = "";

        // Loop through the command-line arguments, starting from index 1 (skipping the command itself)
        // The loop increments by 2 each time because we expect arguments to be in pairs (flag and value)
        for (int i = 1; i < args.Length; i += 2)
        {
            // Use a switch statement to check the current argument (expected to be a flag)
            switch (args[i])
            {
                case "-f":
                    // If the flag is "-f", the next argument is the folder path
                    folder = args[i + 1];
                    break;
                case "-t":
                    // If the flag is "-t", the next argument is the indexer type
                    indexer = args[i + 1];
                    break;
                case "-dis":
                    // If the flag is "-dis", the next argument is the distance metric
                    distance = args[i + 1];
                    break;
            }
        }

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

        if (indexer.ToLower() != "tf-idf")
        {
            Console.WriteLine("Error: Only TF-IDF indexer is currently supported.");
            return;
        }

        distanceMetric = distance;

        // Process documents
        DocumentProcessor processor = new DocumentProcessor();
        var documents = processor.ProcessDocumentsInFolder(folder);

        // Organize words
        var wordLists = OrganizeWords.CreateWordLists(documents);
        var uniqueWords = OrganizeWords.Unique_Words(OrganizeWords.String_to_List(documents));
        var wordOccurrences = OrganizeWords.CountWordOccurrences(wordLists, uniqueWords);
        var documentFrequency = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);

        // Compute TF-IDF
        TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
        var tf = tfIdf.ComputeTF(wordOccurrences);
        var idf = tfIdf.ComputeIDF(wordOccurrences, documentFrequency);
        var tf_idf = tfIdf.ComputeTF_IDF(tf, idf);

        // Save TF-IDF to JSON
        tfIdf.SaveTF_IDFToJson(tf_idf, folder);

        Console.WriteLine($"Indexing complete for folder: {folder}");
        Console.WriteLine($"Using indexer: {indexer}");
        Console.WriteLine($"Distance metric: {distance}");
    }

    static void HandleLoad(string[] args)
    {
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
        if (args.Length != 4 || args[1][0] != '-' || args[3][0] != '-')
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
        if (!int.TryParse(args[3].Substring(1), out int k))
        {
            Console.WriteLine("Error: Invalid value for k. Must be an integer.");
            return;
        }

        // Display information about the search
        Console.WriteLine($"Searching for: {query}");
        Console.WriteLine($"Top {k} results:");
        Console.WriteLine($"Using distance metric: {distanceMetric}");

        // TODO: Implement actual search logic here
        // This should use the loaded index, query, k value, and distance metric
        // to perform the search and return results

        // Placeholder: Simulate displaying the top k search results
        // In a real implementation, this would be replaced with actual search results
        for (int i = 1; i <= k; i++)
        {
            Console.WriteLine($"Result {i}: [Filename] (Score: [Score])");
        }
    }
}
