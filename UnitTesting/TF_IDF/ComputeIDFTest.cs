using System;
using System.Collections.Generic;
using Xunit;

class ComputeIDFTest
{
    static void Main()
    {
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            ["doc1"] = new Dictionary<string, int> { ["apple"] = 2, ["banana"] = 1, ["cherry"] = 1 },
            ["doc2"] = new Dictionary<string, int> { ["apple"] = 1, ["cherry"] = 3 }
        };

        var documentFrequency = new Dictionary<string, int>
        {
            ["apple"] = 2,
            ["banana"] = 1,
            ["cherry"] = 2
        };

        var result = TF_IDF.ComputeIDF(wordOccurrences, documentFrequency);

        Console.WriteLine("ComputeIDF Result:");
        foreach (var word in result)
        {
            Console.WriteLine($"{word.Key}: {word.Value}");
        }
    }
}
