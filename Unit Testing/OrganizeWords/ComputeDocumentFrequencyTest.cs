using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class ComputeDocumentFrequencyTest
{
    private readonly ITestOutputHelper _output;

    public ComputeDocumentFrequencyTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestComputeDocumentFrequency()
    {
        _output.WriteLine("Running ComputeDocumentFrequency test:");

        // Arrange
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {"doc1", new Dictionary<string, int> {{"hello", 2}, {"world", 1}, {"test", 0}}},
            {"doc2", new Dictionary<string, int> {{"hello", 0}, {"world", 1}, {"test", 2}}}
        };

        // Act
        var result = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(1, result["hello"]);
        Assert.Equal(2, result["world"]);
        Assert.Equal(1, result["test"]);

        // Log output for verification
        _output.WriteLine($"Number of unique words: {result.Count}");
        foreach (var word in result)
        {
            _output.WriteLine($"{word.Key}: {word.Value}");
        }
        _output.WriteLine("Test completed.");
    }
}
