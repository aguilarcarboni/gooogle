using System;
using System.IO;
using Xunit;

public class ReadJSONTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ReadJSON test:");
        var test = new ReadJSONTest();
        test.TestReadJSON();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestReadJSON()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "{\"name\":\"John\",\"age\":30}");

        // Act
        var result = processor.ReadJSON(testFilePath);

        // Assert
        Assert.Equal("{\"name\":\"John\",\"age\":30}", result);

        // Clean up
        File.Delete(testFilePath);

        // Console output for manual verification
        Console.WriteLine($"JSON content: {result}");
    }
}
