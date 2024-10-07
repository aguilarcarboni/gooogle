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

        TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
        var result = tfIdf.ComputeTF(wordOccurrences);

        Console.WriteLine("ComputeTF Result:");
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                Console.WriteLine($"  {word.Key}: {word.Value}");
            }
        }

        // Assertions
        Assert.Equal(2, result.Count);
        Assert.Equal(0.5, result["doc1"]["apple"]);
        Assert.Equal(0.25, result["doc1"]["banana"]);
        Assert.Equal(0.25, result["doc1"]["cherry"]);
        Assert.Equal(0.25, result["doc2"]["apple"]);
        Assert.Equal(0.75, result["doc2"]["cherry"]);

        Console.WriteLine("All tests passed!");
    }
}
