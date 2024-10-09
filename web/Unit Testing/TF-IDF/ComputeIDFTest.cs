using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class ComputeIDFTest
{
    private readonly ITestOutputHelper _output;
    private readonly TF_IDF.tf_idf _tfIdf;

    public ComputeIDFTest(ITestOutputHelper output)
    {
        _output = output;
        _tfIdf = new TF_IDF.tf_idf();
    }

    [Fact]
    public void TestComputeIDF()
    {
        _output.WriteLine("Running ComputeIDF test:");

        // Arrange
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {"doc1", new Dictionary<string, int> {{"hello", 2}, {"world", 1}}},
            {"doc2", new Dictionary<string, int> {{"hello", 1}, {"test", 2}}}
        };
        var documentFrequency = new Dictionary<string, int>
        {
            {"hello", 2},
            {"world", 1},
            {"test", 1}
        };

        // Act
        var result = _tfIdf.ComputeIDF(wordOccurrences, documentFrequency);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(0, result["hello"], 3);
        Assert.Equal(Math.Log(2), result["world"], 3);
        Assert.Equal(Math.Log(2), result["test"], 3);

        // Log output for verification
        _output.WriteLine("IDF results:");
        foreach (var word in result)
        {
            _output.WriteLine($"{word.Key}: {word.Value}");
        }
        _output.WriteLine("Test completed.");
    }
}
