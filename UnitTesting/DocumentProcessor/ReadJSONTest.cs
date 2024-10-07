using System;
using Xunit;

class ReadJSONTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string filePath = @"C:\path\to\test\file.json"; // Replace with an actual JSON file path

        string result = processor.ReadJSON(filePath);

        Console.WriteLine("ReadJSON Result:");
        Console.WriteLine($"Content length: {result.Length}");
        Console.WriteLine($"First 100 characters: {result.Substring(0, Math.Min(100, result.Length))}");
    }
}
