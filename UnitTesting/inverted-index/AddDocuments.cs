using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

public class AddDocumentsTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running AddDocuments_PopulateIndex test:");
        var test = new AddDocumentsTest();
        test.TestAddDocuments_PopulateIndex();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestAddDocuments_PopulateIndex()
    {
        // Arrange
        var invertedIndex = new InvertedIndex();
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world"}},
            {"doc2", new List<string> {"hello", "xunit"}}
        };

        // Act
        invertedIndex.AddDocuments(wordLists);

        // Assert
        var indexField = typeof(InvertedIndex).GetField("index", BindingFlags.NonPublic | BindingFlags.Instance);
        var index = (Dictionary<string, HashSet<string>>)indexField.GetValue(invertedIndex);

        Assert.Equal(3, index.Count);
        Assert.Equal(new HashSet<string> {"doc1", "doc2"}, index["hello"]);
        Assert.Equal(new HashSet<string> {"doc1"}, index["world"]);
        Assert.Equal(new HashSet<string> {"doc2"}, index["xunit"]);

        // Console output for manual verification
        Console.WriteLine($"Index count: {index.Count}");
        Console.WriteLine($"'hello' in docs: {string.Join(", ", index["hello"])}");
        Console.WriteLine($"'world' in docs: {string.Join(", ", index["world"])}");
        Console.WriteLine($"'xunit' in docs: {string.Join(", ", index["xunit"])}");
    }
}
