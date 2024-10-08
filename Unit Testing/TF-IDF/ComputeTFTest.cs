using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class ComputeTFTest
{
    private readonly ITestOutputHelper _output;
    private readonly TF_IDF.tf_idf _tfIdf;

    public ComputeTFTest(ITestOutputHelper output)
    {
        _output = output;
        _tfIdf = new TF_IDF.tf_idf();
    }

    [Fact]
    public void TestComputeTF()
    {
        _output.WriteLine("Running ComputeTF test:");

        // Arrange
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {"doc1", new Dictionary<string, int> {{"hello", 2}, {"world", 1}}},
            {"doc2", new Dictionary<string, int> {{"hello", 1}, {"test", 2}}}
        };

        // Act
        var result = _tfIdf.ComputeTF(wordOccurrences);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2.0/3, result["doc1"]["hello"], 3);
        Assert.Equal(1.0/3, result["doc1"]["world"], 3);
        Assert.Equal(1.0/3, result["doc2"]["hello"], 3);
        Assert.Equal(2.0/3, result["doc2"]["test"], 3);

        // Log output for verification
        _output.WriteLine("TF results:");
        foreach (var doc in result)
        {
            _output.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                _output.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
        _output.WriteLine("Test completed.");
    }
}
