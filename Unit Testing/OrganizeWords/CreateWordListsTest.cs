using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class CreateWordListsTest
{
    private readonly ITestOutputHelper _output;

    public CreateWordListsTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestCreateWordLists()
    {
        _output.WriteLine("Running CreateWordLists test:");

        // Arrange
        var documents = new Dictionary<string, string>
        {
            {"doc1", "Hello world!"},
            {"doc2", "Test, test, and more test."}
        };

        // Act
        var result = OrganizeWords.CreateWordLists(documents);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.True(result.ContainsKey("doc1"));
        Assert.True(result.ContainsKey("doc2"));
        Assert.Equal(2, result["doc1"].Count);
        Assert.Equal(5, result["doc2"].Count);

        // Log output for verification
        _output.WriteLine($"Number of documents: {result.Count}");
        foreach (var doc in result)
        {
            _output.WriteLine($"Document: {doc.Key}, Word count: {doc.Value.Count}");
        }
        _output.WriteLine("Test completed.");
    }
}
