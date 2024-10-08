using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class SaveToJson_CreateJsonFileTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running SaveToJson_CreateJsonFile test:");
        var test = new SaveToJson_CreateJsonFileTest();
        test.TestSaveToJson_CreateJsonFile();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestSaveToJson_CreateJsonFile()
    {
        // Arrange
        var invertedIndex = new InvertedIndex();
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world"}},
            {"doc2", new List<string> {"hello", "xunit"}}
        };
        invertedIndex.AddDocuments(wordLists);

        string folderName = "TestFolder";
        string expectedFilePath = Path.Combine("InvertedIndex", $"{folderName}_inverted_index.json");

        // Act
        invertedIndex.SaveToJson(folderName);

        // Assert
        Assert.True(File.Exists(expectedFilePath));
        string jsonContent = File.ReadAllText(expectedFilePath);
        Assert.Contains("hello", jsonContent);
        Assert.Contains("world", jsonContent);
        Assert.Contains("xunit", jsonContent);

        // Console output for manual verification
        Console.WriteLine($"File created: {File.Exists(expectedFilePath)}");
        Console.WriteLine($"JSON content: {jsonContent}");

        // Clean up
        File.Delete(expectedFilePath);
        Directory.Delete("InvertedIndex", false);
    }
}
