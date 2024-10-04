using System;
using System.Collections.Generic;
using System.Linq;

public class Drive
{
    private List<Document> documents;

    public Drive()
    {
        documents = new List<Document>();
    }

    public void AddDocument(Document document)
    {
        if (document != null && document.IsValid())
        {
            documents.Add(document);
        }
        else
        {
            throw new ArgumentException("Invalid document");
        }
    }

    public void RemoveDocument(string fileName)
    {
        var documentToRemove = documents.FirstOrDefault(d => d.FileName == fileName);
        if (documentToRemove != null)
        {
            documents.Remove(documentToRemove);
        }
    }

    public Document GetDocument(string fileName)
    {
        return documents.FirstOrDefault(d => d.FileName == fileName);
    }

    public IEnumerable<Document> GetAllDocuments()
    {
        return documents.AsReadOnly();
    }

    public int DocumentCount => documents.Count;

    public List<string> GetAllUniqueWords()
    {
        return documents
            .SelectMany(d => d.GetUniqueWords())
            .Distinct()
            .ToList();
    }

    public List<Dictionary<string, int>> GetWordOccurrences()
    {
        var allUniqueWords = GetAllUniqueWords();
        return WordOrganizer.CountWordOccurrences(
            documents.Select(d => d.ContentToList()).ToList(),
            allUniqueWords
        );
    }

    public Dictionary<string, int> GetDocumentFrequency()
    {
        return WordOrganizer.ComputeDocumentFrequency(GetWordOccurrences());
    }

    public List<Dictionary<string, double>> ComputeTF()
    {
        return TF_IDF.ComputeTF(GetWordOccurrences());
    }

    public Dictionary<string, double> ComputeIDF()
    {
        return TF_IDF.ComputeIDF(GetWordOccurrences(), GetDocumentFrequency());
    }
}
