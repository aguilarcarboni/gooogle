using System;
using System.Collections.Generic;
using Xunit;

public class UniqueWordsTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running Unique_Words test:");
        var test = new UniqueWordsTest();
        test.TestUniqueWords();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestUniqueWords()
    {
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

        // Console output for manual verification
        Console.WriteLine($"Unique words count: {result.Count}");
        Console.WriteLine($"Unique words: {string.Join(", ", result)}");
    }
}
