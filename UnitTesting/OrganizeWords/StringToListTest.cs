using System;
using System.Collections.Generic;
using Xunit;

public class StringToListTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running String_to_List test:");
        var test = new StringToListTest();
        test.TestStringToList();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestStringToList()
    {
        // Arrange
        var documents = new Dictionary<string, string>
        {
            {"doc1", "Hello world! This is a test."},
            {"doc2", "Another test document."}
        };

        // Act
        var result = OrganizeWords.String_to_List(documents);

        // Assert
        Assert.Equal(9, result.Count);
        Assert.Contains("Hello", result);
        Assert.Contains("world", result);
        Assert.Contains("This", result);
        Assert.Contains("is", result);
        Assert.Contains("a", result);
        Assert.Contains("test", result);
        Assert.Contains("Another", result);
        Assert.Contains("document", result);

        // Console output for manual verification
        Console.WriteLine($"Result count: {result.Count}");
        Console.WriteLine($"Words: {string.Join(", ", result)}");
    }
}
