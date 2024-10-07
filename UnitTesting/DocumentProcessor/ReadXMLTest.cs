using System;
using Xunit;

class ReadXMLTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string filePath = @"C:\path\to\test\file.xml"; // Replace with an actual XML file path

        string result = processor.ReadXML(filePath);

        Console.WriteLine("ReadXML Result:");
        Console.WriteLine($"Content length: {result.Length}");
        Console.WriteLine($"First 100 characters: {result.Substring(0, Math.Min(100, result.Length))}");
    }
}
