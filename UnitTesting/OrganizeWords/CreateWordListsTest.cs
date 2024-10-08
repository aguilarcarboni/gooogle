using System;
using System.Collections.Generic;
using Xunit;

public class CreateWordListsTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running CreateWordLists test:");
        var test = new CreateWordListsTest();
        test.TestCreateWordLists();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestCreateWordLists()
    {
        // Arrange
        var documents = new Dictionary<string, string>
        {
            {"doc1", "Hello world! This is a test."},
            {"doc2", "Another test document."}
        };

        // Act
        var result = OrganizeWords.CreateWordLists(documents);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.True(result.ContainsKey("doc1"));
        Assert.True(result.ContainsKey("doc2"));
        Assert.Equal(6, result["doc1"].Count);
        Assert.Equal(3, result["doc2"].Count);

        // Console output for manual verification
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            Console.WriteLine($"Words: {string.Join(", ", doc.Value)}");
        }
    }
}
