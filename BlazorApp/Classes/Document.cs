using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class Document
{
    public string FileName { get; private set; }
    public string Content { get; private set; }
    public string FileType { get; private set; }
    public List<string> Words { get; private set; }
    public Dictionary<string, int> WordCounts { get; private set; }

    private static readonly HashSet<string> SupportedFileTypes = new HashSet<string>
    {
        "txt", "csv", "xml", "json", "html"
    };

    private static readonly HashSet<string> StopWords = new HashSet<string>
    {
        "a", "an", "and", "are", "as", "at", "be", "by", "for", "from",
        "has", "he", "in", "is", "it", "its", "of", "on", "that", "the",
        "to", "was", "were", "will", "with"
    };

    public Document(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }

        FileName = Path.GetFileName(filePath);
        FileType = Path.GetExtension(filePath).TrimStart('.').ToLower();

        if (!SupportedFileTypes.Contains(FileType))
        {
            throw new ArgumentException($"Unsupported file type: {FileType}");
        }

        Content = ReadFileContent(filePath);
        Words = ProcessContent();
        WordCounts = CountWords();
    }

    private string ReadFileContent(string filePath)
    {
        switch (FileType)
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
                throw new ArgumentException($"Unsupported file type: {FileType}");
        }
    }

    private string ReadCSV(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        return string.Join(" ", lines.Select(line => string.Join(" ", line.Split(','))));
    }

    private string ReadXML(string filePath)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(filePath);
        return doc.OuterXml;
    }

    private string ReadJSON(string filePath)
    {
        string jsonContent = File.ReadAllText(filePath);
        using JsonDocument doc = JsonDocument.Parse(jsonContent);
        return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = false });
    }

    private string ReadHTML(string filePath)
    {
        string htmlContent = File.ReadAllText(filePath);
        string textContent = Regex.Replace(htmlContent, "<[^>]+>", string.Empty);
        textContent = Regex.Replace(textContent, @"\s+", " ").Trim();
        return System.Net.WebUtility.HtmlDecode(textContent);
    }

    private List<string> ProcessContent()
    {
        char[] delimiters = { ',', ';', ' ', '.', '!', '?', '\n', '\r', '\t', '<', '>', '(', ')', '[', ']', '{', '}', '/', '\\', '*', '&', '^', '%', '$', '#', '@', '~', '|', '=', '+', '\'', '\"', ':', '?', '!' };
        return Content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word.ToLower())
            .Where(word => !StopWords.Contains(word) && word.Length > 1)
            .ToList();
    }

    private Dictionary<string, int> CountWords()
    {
        return Words.GroupBy(w => w)
            .ToDictionary(g => g.Key, g => g.Count());
    }

    public override string ToString()
    {
        return $"FileName: {FileName}, FileType: {FileType}, Word Count: {Words.Count}";
    }
}