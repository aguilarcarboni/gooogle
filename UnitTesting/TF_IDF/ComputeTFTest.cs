using System;
using System.Collections.Generic;
using Xunit;

class ComputeTFTest
{
    static void Main()
    {
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            ["doc1"] = new Dictionary<string, int> { ["apple"] = 2, ["banana"] = 1, ["cherry"] = 1 },
            ["doc2"] = new Dictionary<string, int> { ["apple"] = 1, ["cherry"] = 3 }
        };

        var result = TF_IDF.ComputeTF(wordOccurrences);

        Console.WriteLine("ComputeTF Result:");
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                Console.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
    }
}
