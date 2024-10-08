using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ProcessDocumentsInFolderTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ProcessDocumentsInFolderTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestProcessDocumentsInFolder()
    {
        _output.WriteLine("Running ProcessDocumentsInFolder test:");

        // Arrange
        string testFolderPath = Path.Combine(Path.GetTempPath(), "TestDocuments");
        Directory.CreateDirectory(testFolderPath);
        File.WriteAllText(Path.Combine(testFolderPath, "test1.txt"), "Hello World");
        File.WriteAllText(Path.Combine(testFolderPath, "test2.csv"), "Name,Age\nJohn,30");

        // Act
        var result = _processor.ProcessDocumentsInFolder(testFolderPath);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.True(result.ContainsKey("test1.txt"));
        Assert.True(result.ContainsKey("test2.csv"));

        // Log output for verification
        _output.WriteLine($"Number of processed documents: {result.Count}");
        foreach (var doc in result)
        {
            _output.WriteLine($"Document: {doc.Key}, Content: {doc.Value}");
        }
        _output.WriteLine("Test completed.");

        // Clean up
        Directory.Delete(testFolderPath, true);
    }
}
