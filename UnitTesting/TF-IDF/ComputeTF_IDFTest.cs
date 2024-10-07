using System;
using System.Collections.Generic;
using Xunit;

class ComputeTF_IDFTest
{
    static void Main()
    {
        var tf = new Dictionary<string, Dictionary<string, double>>
        {
            ["doc1"] = new Dictionary<string, double> { ["apple"] = 0.5, ["banana"] = 0.25, ["cherry"] = 0.25 },
            ["doc2"] = new Dictionary<string, double> { ["apple"] = 0.25, ["cherry"] = 0.75 }
        };

        var idf = new Dictionary<string, double>
        {
            ["apple"] = 0,
            ["banana"] = Math.Log(2.0),
            ["cherry"] = 0
        };

        TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
        var result = tfIdf.ComputeTF_IDF(tf, idf);

        Console.WriteLine("ComputeTF_IDF Result:");
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
        Assert.Equal(0, result["doc1"]["apple"]);
        Assert.Equal(0.25 * Math.Log(2.0), result["doc1"]["banana"]);
        Assert.Equal(0, result["doc1"]["cherry"]);
        Assert.Equal(0, result["doc2"]["apple"]);
        Assert.Equal(0, result["doc2"]["banana"]);
        Assert.Equal(0, result["doc2"]["cherry"]);

        Console.WriteLine("All tests passed!");
    }
}
