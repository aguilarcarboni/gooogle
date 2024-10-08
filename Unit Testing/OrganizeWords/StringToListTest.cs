using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class StringToListTest
{
    private readonly ITestOutputHelper _output;

    public StringToListTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestStringToList()
    {
        _output.WriteLine("Running String_to_List test:");

        // Arrange
        var documents = new Dictionary<string, string>
        {
            {"doc1", "Hello world!"},
            {"doc2", "Test, test, and more test."}
        };

        // Act
        var result = OrganizeWords.String_to_List(documents);

        // Assert
        Assert.Equal(7, result.Count);
        Assert.Contains("Hello", result);
        Assert.Contains("world", result);
        Assert.Contains("Test", result);
        Assert.Contains("test", result);
        Assert.Contains("and", result);
        Assert.Contains("more", result);

        // Log output for verification
        _output.WriteLine($"Word count: {result.Count}");
        _output.WriteLine($"Words: {string.Join(", ", result)}");
        _output.WriteLine("Test completed.");
    }
}
