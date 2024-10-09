using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ReadCSVTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ReadCSVTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestReadCSV()
    {
        _output.WriteLine("Running ReadCSV test:");

        // Arrange
        string testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "Name,Age\nJohn,30\nJane,25");

        // Act
        var result = _processor.ReadCSV(testFilePath);

        // Assert
        Assert.Equal("Name Age John 30 Jane 25", result);

        // Log output for verification
        _output.WriteLine($"CSV content: {result}");
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(testFilePath);
    }
}
