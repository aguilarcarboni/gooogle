using System;
using System.Collections.Generic;
using Xunit;

public class ComputeTF_IDFTest
{
    public static void Main(string[] args)
    {
        new ComputeTF_IDFTest().TestComputeTF_IDF();
    }

    [Fact]
    public void TestComputeTF_IDF()
    {
        Console.WriteLine("Running ComputeTF_IDF test:");

        // Arrange
        var tf_idf = new TF_IDF.tf_idf();
        var tf = new Dictionary<string, Dictionary<string, double>>
        {
            {"doc1", new Dictionary<string, double> {{"hello", 2.0/3}, {"world", 1.0/3}}},
            {"doc2", new Dictionary<string, double> {{"hello", 0.5}, {"test", 0.5}}}
        };
        var idf = new Dictionary<string, double>
        {
            {"hello", 0},
            {"world", Math.Log(2)},
            {"test", Math.Log(2)}
        };

        // Act
        var result = tf_idf.ComputeTF_IDF(tf, idf);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(0, result["doc1"]["hello"], 3);
        Assert.Equal(Math.Log(2)/3, result["doc1"]["world"], 3);
        Assert.Equal(0, result["doc1"]["test"], 3);
        Assert.Equal(0, result["doc2"]["hello"], 3);
        Assert.Equal(0, result["doc2"]["world"], 3);
        Assert.Equal(0.5 * Math.Log(2), result["doc2"]["test"], 3);

        // Console output for manual verification
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                Console.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
        Console.WriteLine("Test completed.");
    }
}
