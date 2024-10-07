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

        TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
        var result = tfIdf.ComputeIDF(wordOccurrences, documentFrequency);

        Console.WriteLine("ComputeIDF Result:");
        foreach (var word in result)
        {
            Console.WriteLine($"{word.Key}: {word.Value}");
        }

        // Assertions
        Assert.Equal(3, result.Count);
        Assert.Equal(0, result["apple"]);
        Assert.Equal(Math.Log(2.0), result["banana"]);
        Assert.Equal(0, result["cherry"]);

        Console.WriteLine("All tests passed!");
    }
}
