using System;
using System.Collections.Generic;
using Xunit;

public class CountWordOccurrencesTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running CountWordOccurrences test:");
        var test = new CountWordOccurrencesTest();
        test.TestCountWordOccurrences();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestCountWordOccurrences()
    {
        // Arrange
        var docs = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world", "hello"}},
            {"doc2", new List<string> {"test", "world", "document"}}
        };
        var allUniqueWords = new List<string> {"hello", "world", "test", "document"};

        // Act
        var result = OrganizeWords.CountWordOccurrences(docs, allUniqueWords);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(2, result["doc1"]["hello"]);
        Assert.Equal(1, result["doc1"]["world"]);
        Assert.Equal(0, result["doc1"]["test"]);
        Assert.Equal(1, result["doc2"]["world"]);
        Assert.Equal(1, result["doc2"]["test"]);
        Assert.Equal(1, result["doc2"]["document"]);

        // Console output for manual verification
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                Console.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
    }
}
