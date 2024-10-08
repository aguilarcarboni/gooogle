using System;
using System.Collections.Generic;
using Xunit;

public class ComputeIDFTest
{
    public static void Main(string[] args)
    {
        new ComputeIDFTest().TestComputeIDF();
    }

    [Fact]
    public void TestComputeIDF()
    {
        Console.WriteLine("Running ComputeIDF test:");

        // Arrange
        var tf_idf = new TF_IDF.tf_idf();
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {"doc1", new Dictionary<string, int> {{"hello", 2}, {"world", 1}}},
            {"doc2", new Dictionary<string, int> {{"hello", 1}, {"test", 1}}}
        };
        var documentFrequency = new Dictionary<string, int>
        {
            {"hello", 2},
            {"world", 1},
            {"test", 1}
        };

        // Act
        var result = tf_idf.ComputeIDF(wordOccurrences, documentFrequency);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(0, result["hello"], 3);
        Assert.Equal(Math.Log(2), result["world"], 3);
        Assert.Equal(Math.Log(2), result["test"], 3);

        // Console output for manual verification
        foreach (var word in result)
        {
            Console.WriteLine($"{word.Key}: {word.Value}");
        }
        Console.WriteLine("Test completed.");
    }
}
