using System;
using Xunit;

class ReadHTMLTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string filePath = @"C:\path\to\test\file.html"; // Replace with an actual HTML file path

        string result = processor.ReadHTML(filePath);

        Console.WriteLine("ReadHTML Result:");
        Console.WriteLine($"Content length: {result.Length}");
        Console.WriteLine($"First 100 characters: {result.Substring(0, Math.Min(100, result.Length))}");
    }
}
