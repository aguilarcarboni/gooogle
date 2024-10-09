using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ReadHTMLTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ReadHTMLTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestReadHTML()
    {
        _output.WriteLine("Running ReadHTML test:");

        // Arrange
        string testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "<html><body><p>Hello World</p></body></html>");

        // Act
        var result = _processor.ReadHTML(testFilePath);

        // Assert
        Assert.Equal("Hello World", result);

        // Log output for verification
        _output.WriteLine($"HTML content: {result}");
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(testFilePath);
    }
}
