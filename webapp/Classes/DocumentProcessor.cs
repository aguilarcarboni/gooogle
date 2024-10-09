using System;
using System.Collections.Generic;
using System.IO;

public interface IDocumentProcessor
{
    DocumentCollection ProcessDocumentsInFolder(string folderPath);
}

public class DocumentProcessor : IDocumentProcessor
{
    public DocumentCollection ProcessDocumentsInFolder(string folderPath)
    {
        DocumentCollection collection = new DocumentCollection();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"The folder {folderPath} does not exist.");
            return collection;
        }

        string[] filePaths = Directory.GetFiles(folderPath);

        foreach (string filePath in filePaths)
        {
            try
            {
                Document document = new Document(filePath);
                collection.AddDocument(document);
                Console.WriteLine($"Processed file: {document.FileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
            }
        }

        Console.WriteLine($"Total documents processed: {collection.Documents.Count}");
        return collection;
    }
}