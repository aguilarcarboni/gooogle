using System;
using System.IO;
using Xunit;
using Xunit.Abstractions;

public class ReadJSONTest
{
    private readonly ITestOutputHelper _output;
    private readonly DocumentProcessor _processor;

    public ReadJSONTest(ITestOutputHelper output)
    {
        _output = output;
        _processor = new DocumentProcessor();
    }

    [Fact]
    public void TestReadJSON()
    {
        _output.WriteLine("Running ReadJSON test:");

        // Arrange
        string testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "{\"name\":\"John\",\"age\":30}");

        // Act
        var result = _processor.ReadJSON(testFilePath);

        // Assert
        Assert.Equal("{\"name\":\"John\",\"age\":30}", result);

        // Log output for verification
        _output.WriteLine($"JSON content: {result}");
        _output.WriteLine("Test completed.");

        // Clean up
        File.Delete(testFilePath);
    }
}
