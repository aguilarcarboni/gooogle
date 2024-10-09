using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

public class AddDocumentsTest
{
    private readonly ITestOutputHelper _output;
    private readonly InvertedIndex _invertedIndex;

    public AddDocumentsTest(ITestOutputHelper output)
    {
        _output = output;
        _invertedIndex = new InvertedIndex();
    }

    [Fact]
    public void TestAddDocuments()
    {
        _output.WriteLine("Running AddDocuments test:");

        // Arrange
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world"}},
            {"doc2", new List<string> {"hello", "test"}}
        };

        // Act
        _invertedIndex.AddDocuments(wordLists);

        // Assert
        // We need to use reflection to access the private index field
        var indexField = typeof(InvertedIndex).GetField("index", BindingFlags.NonPublic | BindingFlags.Instance);
        var index = (Dictionary<string, HashSet<string>>)indexField.GetValue(_invertedIndex);

        Assert.Equal(3, index.Count);
        Assert.Equal(new HashSet<string> {"doc1", "doc2"}, index["hello"]);
        Assert.Equal(new HashSet<string> {"doc1"}, index["world"]);
        Assert.Equal(new HashSet<string> {"doc2"}, index["test"]);

        // Log output for verification
        _output.WriteLine("Inverted Index contents:");
        foreach (var entry in index)
        {
            _output.WriteLine($"Word: {entry.Key}, Documents: {string.Join(", ", entry.Value)}");
        }
        _output.WriteLine("Test completed.");
    }
}
