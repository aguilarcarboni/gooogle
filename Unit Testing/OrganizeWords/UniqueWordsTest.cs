using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class UniqueWordsTest
{
    private readonly ITestOutputHelper _output;

    public UniqueWordsTest(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestUniqueWords()
    {
        _output.WriteLine("Running Unique_Words test:");

        // Arrange
        var documents = new List<string> { "Hello", "world", "Hello", "test", "a", "the", "test" };

        // Act
        var result = OrganizeWords.Unique_Words(documents);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Contains("hello", result);
        Assert.Contains("world", result);
        Assert.Contains("test", result);
        Assert.DoesNotContain("a", result);
        Assert.DoesNotContain("the", result);

        // Log output for verification
        _output.WriteLine($"Unique words count: {result.Count}");
        _output.WriteLine($"Unique words: {string.Join(", ", result)}");
        _output.WriteLine("Test completed.");
    }
}
