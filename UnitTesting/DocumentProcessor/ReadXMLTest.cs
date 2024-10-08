using System;
using System.IO;
using Xunit;

public class ReadXMLTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ReadXML test:");
        var test = new ReadXMLTest();
        test.TestReadXML();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestReadXML()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "<root><item>Test</item></root>");

        // Act
        var result = processor.ReadXML(testFilePath);

        // Assert
        Assert.Equal("<root><item>Test</item></root>", result);

        // Clean up
        File.Delete(testFilePath);

        // Console output for manual verification
        Console.WriteLine($"XML content: {result}");
    }
}
