using System;
using Xunit;

class ProcessDocumentsInFolderTest
{
    static void Main()
    {
        DocumentProcessor processor = new DocumentProcessor();
        string folderPath = @"C:\path\to\test\folder"; // Replace with an actual folder path

        Dictionary<string, string> result = processor.ProcessDocumentsInFolder(folderPath);

        Console.WriteLine("ProcessDocumentsInFolder Result:");
        foreach (var doc in result)
        {
            Console.WriteLine($"File: {doc.Key}, Content length: {doc.Value.Length}");
        }
    }
}
