using System;
using System.Collections.Generic;
using Xunit;

class ReadCSVTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string filePath = @"C:\path\to\test\file.csv"; // Replace with an actual CSV file path

        string result = processor.ReadCSV(filePath);

        Console.WriteLine("ReadCSV Result:");
        Console.WriteLine($"Content length: {result.Length}");
        Console.WriteLine($"First 100 characters: {result.Substring(0, Math.Min(100, result.Length))}");
    }
}
