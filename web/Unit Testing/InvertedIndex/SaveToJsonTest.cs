using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

public class SaveToJsonTest
{
    private readonly ITestOutputHelper _output;
    private readonly InvertedIndex _invertedIndex;

    public SaveToJsonTest(ITestOutputHelper output)
    {
        _output = output;
        _invertedIndex = new InvertedIndex();
    }

    [Fact]
    public void TestSaveToJson()
    {
        _output.WriteLine("Running SaveToJson test:");

        // Arrange
        var wordLists = new Dictionary<string, List<string>>
        {
            {"doc1", new List<string> {"hello", "world"}},
            {"doc2", new List<string> {"hello", "test"}}
        };
        _invertedIndex.AddDocuments(wordLists);

        string testFolderName = "TestFolder";
        string expectedFilePath = Path.Combine("InvertedIndex", $"{testFolderName}_inverted_index.json");

        // Act
        _invertedIndex.SaveToJson(testFolderName);

        // Assert
        Assert.True(File.Exists(expectedFilePath));

        // Read and parse the JSON file
        string jsonContent = File.ReadAllText(expectedFilePath);
        var deserializedIndex = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonContent);

        Assert.Equal(3, deserializedIndex.Count);
        Assert.Equal(new List<string> {"doc1", "doc2"}, deserializedIndex["hello"]);
        Assert.Equal(new List<string> {"doc1"}, deserializedIndex["world"]);
        Assert.Equal(new List<string> {"doc2"}, deserializedIndex["test"]);

        // Log output for verification
        _output.WriteLine($"JSON file created at: {expectedFilePath}");
        _output.WriteLine("JSON content:");
        _output.WriteLine(jsonContent);
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(expectedFilePath);
        Directory.Delete("InvertedIndex");
    }
}
