using System;
using System.IO;
using Xunit;

public class ReadHTMLTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ReadHTML test:");
        var test = new ReadHTMLTest();
        test.TestReadHTML();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestReadHTML()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "<html><body><p>Hello, world!</p></body></html>");

        // Act
        var result = processor.ReadHTML(testFilePath);

        // Assert
        Assert.Equal("Hello, world!", result);

        // Clean up
        File.Delete(testFilePath);

        // Console output for manual verification
        Console.WriteLine($"HTML content: {result}");
    }
}
