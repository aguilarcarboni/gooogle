using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class SaveTF_IDFToJsonTest
{
    public static void Main(string[] args)
    {
        new SaveTF_IDFToJsonTest().TestSaveTF_IDFToJson();
    }

    [Fact]
    public void TestSaveTF_IDFToJson()
    {
        Console.WriteLine("Running SaveTF_IDFToJson test:");

        // Arrange
        var tf_idf = new TF_IDF.tf_idf();
        var tf_idf_data = new Dictionary<string, Dictionary<string, double>>
        {
            {"doc1", new Dictionary<string, double> {{"hello", 0}, {"world", Math.Log(2)/3}}},
            {"doc2", new Dictionary<string, double> {{"hello", 0}, {"test", 0.5 * Math.Log(2)}}}
        };
        var sourceFolder = "TestFolder";

        // Act
        tf_idf.SaveTF_IDFToJson(tf_idf_data, sourceFolder);

        // Assert
        string expectedFilePath = Path.Combine("TF-IDF", $"{sourceFolder}_tf_idf.json");
        Assert.True(File.Exists(expectedFilePath));

        // Console output for manual verification
        Console.WriteLine($"File created: {expectedFilePath}");
        Console.WriteLine($"File content: {File.ReadAllText(expectedFilePath)}");
        Console.WriteLine("Test completed.");

        // Clean up
        File.Delete(expectedFilePath);
        Directory.Delete("TF-IDF", false);
    }
}
