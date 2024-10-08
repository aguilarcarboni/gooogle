using System;
using System.IO;
using Xunit;

public class ReadFileContentTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ReadFileContent test:");
        var test = new ReadFileContentTest();
        test.TestReadFileContent();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestReadFileContent()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "Hello, world!");

        // Act
        var result = processor.ReadFileContent(testFilePath, "txt");

        // Assert
        Assert.Equal("Hello, world!", result);

        // Clean up
        File.Delete(testFilePath);

        // Console output for manual verification
        Console.WriteLine($"File content: {result}");
    }
}
