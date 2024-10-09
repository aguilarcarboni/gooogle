using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ReadXMLTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ReadXMLTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestReadXML()
    {
        _output.WriteLine("Running ReadXML test:");

        // Arrange
        string testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "<root><item>Test</item></root>");

        // Act
        var result = _processor.ReadXML(testFilePath);

        // Assert
        Assert.Equal("<root><item>Test</item></root>", result);

        // Log output for verification
        _output.WriteLine($"XML content: {result}");
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(testFilePath);
    }
}
