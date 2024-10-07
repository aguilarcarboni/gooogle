using System;
using System.Collections.Generic;
using Xunit;

class ReadFileContentTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string filePath = @"C:\path\to\test\file.txt"; // Replace with an actual file path
        string fileType = "txt"; // Change this to test different file types

        string result = processor.ReadFileContent(filePath, fileType);

        Console.WriteLine("ReadFileContent Result:");
        Console.WriteLine($"Content length: {result.Length}");
        Console.WriteLine($"First 100 characters: {result.Substring(0, Math.Min(100, result.Length))}");
    }
}
