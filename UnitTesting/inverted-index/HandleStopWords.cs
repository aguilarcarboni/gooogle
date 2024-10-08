using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

public class AddDocuments_HandleStopWordsTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running AddDocuments_HandleStopWords test:");
        var test = new AddDocuments_HandleStopWordsTest();
        test.TestAddDocuments_HandleStopWords();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestAddDocuments_HandleStopWords()
    {
        // Arrange
        var invertedIndex = new InvertedIndex();
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"the", "quick", "brown", "fox"}}
        };

        // Act
        invertedIndex.AddDocuments(wordLists);

        // Assert
        var indexField = typeof(InvertedIndex).GetField("index", BindingFlags.NonPublic | BindingFlags.Instance);
        var index = (Dictionary<string, HashSet<string>>)indexField.GetValue(invertedIndex);

        Assert.Equal(3, index.Count);
        Assert.False(index.ContainsKey("the"));
        Assert.True(index.ContainsKey("quick"));
        Assert.True(index.ContainsKey("brown"));
        Assert.True(index.ContainsKey("fox"));

        // Console output for manual verification
        Console.WriteLine($"Index count: {index.Count}");
        Console.WriteLine($"Contains 'the': {index.ContainsKey("the")}");
        Console.WriteLine($"Contains 'quick': {index.ContainsKey("quick")}");
        Console.WriteLine($"Contains 'brown': {index.ContainsKey("brown")}");
        Console.WriteLine($"Contains 'fox': {index.ContainsKey("fox")}");
    }
}
