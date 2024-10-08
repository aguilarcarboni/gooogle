using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ReadFileContentTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ReadFileContentTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestReadFileContent()
    {
        _output.WriteLine("Running ReadFileContent test:");

        // Arrange
        string testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "Hello World");

        // Act
        var result = _processor.ReadFileContent(testFilePath, "txt");

        // Assert
        Assert.Equal("Hello World", result);

        // Log output for verification
        _output.WriteLine($"File content: {result}");
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(testFilePath);
    }
}
