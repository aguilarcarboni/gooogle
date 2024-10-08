using System;
using System.IO;
using Xunit;

public class ReadCSVTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running ReadCSV test:");
        var test = new ReadCSVTest();
        test.TestReadCSV();
        Console.WriteLine("Test completed.");
    }

    [Fact]
    public void TestReadCSV()
    {
        // Arrange
        var processor = new DocumentProcessor();
        var testFilePath = Path.GetTempFileName();
        File.WriteAllText(testFilePath, "Name,Age\nJohn,30\nJane,25");

        // Act
        var result = processor.ReadCSV(testFilePath);

        // Assert
        Assert.Equal("Name Age John 30 Jane 25", result);

        // Clean up
        File.Delete(testFilePath);

        // Console output for manual verification
        Console.WriteLine($"CSV content: {result}");
    }
}
