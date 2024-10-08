using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

public class ProcessDocumentsInFolderTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ProcessDocumentsInFolder test:");
        var test = new ProcessDocumentsInFolderTest();
        test.TestProcessDocumentsInFolder();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestProcessDocumentsInFolder()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFolderPath = Path.Combine(Path.GetTempPath(), "TestDocuments");
        Directory.CreateDirectory(testFolderPath);

        File.WriteAllText(Path.Combine(testFolderPath, "test.txt"), "Hello, world!");
        File.WriteAllText(Path.Combine(testFolderPath, "test.csv"), "Name,Age\nJohn,30");

        // Act
        var result = processor.ProcessDocumentsInFolder(testFolderPath);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains("test.txt", result.Keys);
        Assert.Contains("test.csv", result.Keys);
        Assert.Equal("Hello, world!", result["test.txt"]);
        Assert.Equal("Name Age John 30", result["test.csv"]);

        // Clean up
        Directory.Delete(testFolderPath, true);

        // Console output for manual verification
        foreach (var doc in result)
        {
            Console.WriteLine($"Document: {doc.Key}");
            Console.WriteLine($"Content: {doc.Value}");
        }
    }
}
