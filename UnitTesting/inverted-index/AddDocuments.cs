using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

    static void TestAddDocuments()
    {
        Console.WriteLine("Testing AddDocuments:");
        var invertedIndex = new InvertedIndex();
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world"}},
            {"doc2", new List<string> {"hello", "xunit"}}
        };

        invertedIndex.AddDocuments(wordLists);

        // Use reflection to access private field
        var indexField = typeof(InvertedIndex).GetField("index", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var index = (Dictionary<string, HashSet<string>>)indexField.GetValue(invertedIndex);

        Console.WriteLine($"Index count: {index.Count}");
        Console.WriteLine($"'hello' in docs: {string.Join(", ", index["hello"])}");
        Console.WriteLine($"'world' in docs: {string.Join(", ", index["world"])}");
        Console.WriteLine($"'xunit' in docs: {string.Join(", ", index["xunit"])}");
        Console.WriteLine();
    }
