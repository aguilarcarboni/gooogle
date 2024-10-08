using System;
using System.Collections.Generic;
using Xunit;

public class ComputeTFTest
{
    public static void Main(string[] args)
    {
        new ComputeTFTest().TestComputeTF();
    }

    [Fact]
    public void TestComputeTF()
    {
        Console.WriteLine("Running ComputeTF test:");

        // Arrange
        var tf_idf = new TF_IDF.tf_idf();
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {"doc1", new Dictionary<string, int> {{"hello", 2}, {"world", 1}}},
            {"doc2", new Dictionary<string, int> {{"hello", 1}, {"test", 1}}}
        };

        // Act
        var result = tf_idf.ComputeTF(wordOccurrences);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2.0/3, result["doc1"]["hello"], 3);
        Assert.Equal(1.0/3, result["doc1"]["world"], 3);
        Assert.Equal(0.5, result["doc2"]["hello"], 3);
        Assert.Equal(0.5, result["doc2"]["test"], 3);

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
