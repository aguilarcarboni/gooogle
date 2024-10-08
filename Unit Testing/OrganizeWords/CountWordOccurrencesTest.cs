using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class CountWordOccurrencesTest
{
    private readonly ITestOutputHelper _output;

    public CountWordOccurrencesTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestCountWordOccurrences()
    {
        _output.WriteLine("Running CountWordOccurrences test:");

        // Arrange
        var docs = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world", "hello"}},
            {"doc2", new List<string> {"test", "world", "test"}}
        };
        var allUniqueWords = new List<string> {"hello", "world", "test"};

        // Act
        var result = OrganizeWords.CountWordOccurrences(docs, allUniqueWords);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result["doc1"]["hello"]);
        Assert.Equal(1, result["doc1"]["world"]);
        Assert.Equal(0, result["doc1"]["test"]);
        Assert.Equal(0, result["doc2"]["hello"]);
        Assert.Equal(1, result["doc2"]["world"]);
        Assert.Equal(2, result["doc2"]["test"]);

        // Log output for verification
        _output.WriteLine($"Number of documents: {result.Count}");
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
