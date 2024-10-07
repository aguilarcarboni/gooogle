using System;
using System.Collections.Generic;
using Xunit;

class ComputeDocumentFrequencyTest
{
    static void Main()
    {
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            ["test1"] = new Dictionary<string, int> { ["apple"] = 2, ["banana"] = 1, ["cherry"] = 0 },
            ["test2"] = new Dictionary<string, int> { ["apple"] = 1, ["banana"] = 0, ["cherry"] = 2 }
        };

        var result = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);

        Console.WriteLine("ComputeDocumentFrequency Result:");
        foreach (var word in result)
        {
            Console.WriteLine($"{word.Key}: {word.Value}");
        }
    }
}
