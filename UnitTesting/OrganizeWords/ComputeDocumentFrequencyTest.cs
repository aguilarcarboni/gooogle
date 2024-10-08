using System;
using System.Collections.Generic;
using Xunit;

public class ComputeDocumentFrequencyTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ComputeDocumentFrequency test:");
        var test = new ComputeDocumentFrequencyTest();
        test.TestComputeDocumentFrequency();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestComputeDocumentFrequency()
    {
        // Arrange
        var wordOccurrences = new Dictionary<string, Dictionary<string, int>>
        {
            {
                "doc1", new Dictionary<string, int>
                {
                    {"hello", 2},
                    {"world", 1},
                    {"test", 0}
                }
            },
            {
                "doc2", new Dictionary<string, int>
                {
                    {"hello", 0},
                    {"world", 1},
                    {"test", 1}
                }
            }
        };

        // Act
        var result = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(1, result["hello"]);
        Assert.Equal(2, result["world"]);
        Assert.Equal(1, result["test"]);

        // Console output for manual verification
        foreach (var word in result)
        {
            Console.WriteLine($"{word.Key}: {word.Value}");
        }
    }
}
