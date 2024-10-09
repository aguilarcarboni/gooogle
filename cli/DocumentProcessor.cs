using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

public interface IDocumentProcessor
{
    Dictionary<string, string> ProcessDocumentsInFolder(string folderPath);
    string ReadFileContent(string filePath, string fileType);
    string ReadCSV(string filePath);
    string ReadXML(string filePath);
    string ReadJSON(string filePath);
    string ReadHTML(string filePath);
}

public class DocumentProcessor : IDocumentProcessor
{
    private static readonly HashSet<string> SupportedFileTypes = new HashSet<string>
    {
        "txt", "csv", "xml", "json", "html"
    };

    public Dictionary<string, string> ProcessDocumentsInFolder(string folderPath)
    {
        Dictionary<string, string> documents = new Dictionary<string, string>();

        if (!Directory.Exists(folderPath))
        {
            Console.WriteLine($"The folder {folderPath} does not exist.");
            return documents;
        }

        string[] filePaths = Directory.GetFiles(folderPath);

        foreach (string filePath in filePaths)
        {
            string fileType = Path.GetExtension(filePath).TrimStart('.').ToLower();
            if (SupportedFileTypes.Contains(fileType))
            {
                try
                {
                    string fileName = Path.GetFileName(filePath);
                    string content = ReadFileContent(filePath, fileType);
                    documents[fileName] = content;
                    Console.WriteLine($"Processed file: {fileName}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing file {filePath}: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine($"Unsupported file type: {filePath}");
            }
        }

        Console.WriteLine($"Total documents processed: {documents.Count}");
        return documents;
    }

    public string ReadFileContent(string filePath, string fileType)
    {
        switch (fileType.ToLower())
        {
            case "txt":
                return File.ReadAllText(filePath);
            case "csv":
                return ReadCSV(filePath);
            case "xml":
                return ReadXML(filePath);
            case "json":
                return ReadJSON(filePath);
            case "html":
                return ReadHTML(filePath);
            default:
                throw new ArgumentException($"Unsupported file type: {fileType}");
        }
    }

    public string ReadCSV(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return string.Join(" ", lines.Select(line => string.Join(" ", line.Split(','))));
    }

    public string ReadXML(string filePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        return doc.OuterXml;
    }

    public string ReadJSON(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = false });
    }

    public string ReadHTML(string filePath)
    {
        string htmlContent = File.ReadAllText(filePath);
        string textContent = Regex.Replace(htmlContent, "<[^>]+>", string.Empty);
        textContent = Regex.Replace(textContent, @"\s+", " ").Trim();
        return System.Net.WebUtility.HtmlDecode(textContent);
    }
}