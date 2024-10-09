using System;
using System.Collections.Generic;
using System.Linq;

public class DocumentCollection
{
    public List<Document> Documents { get; set; }
    public HashSet<string> UniqueWords { get; private set; }
    public Dictionary<string, int> DocumentFrequency { get; private set; }

    public DocumentCollection()
    {
        Documents = new List<Document>();
        UniqueWords = new HashSet<string>();
        DocumentFrequency = new Dictionary<string, int>();
    }

    public void AddDocument(Document document)
    {
        Documents.Add(document);
        UpdateUniqueWords(document);
        UpdateDocumentFrequency(document);
    }

    private void UpdateUniqueWords(Document document)
    {
        foreach (var word in document.Words)
        {
            UniqueWords.Add(word);
        }
    }

    private void UpdateDocumentFrequency(Document document)
    {
        foreach (var word in document.WordCounts.Keys)
        {
            if (DocumentFrequency.ContainsKey(word))
            {
                DocumentFrequency[word]++;
            }
            else
            {
                DocumentFrequency[word] = 1;
            }
        }
    }

    public Dictionary<string, Dictionary<string, int>> GetWordOccurrences()
    {
        return Documents.ToDictionary(
            doc => doc.FileName,
            doc => UniqueWords.ToDictionary(
                word => word,
                word => doc.WordCounts.ContainsKey(word) ? doc.WordCounts[word] : 0
            )
        );
    }

    public List<string> GetAllWords()
    {
        return Documents.SelectMany(doc => doc.Words).ToList();
    }

    public List<string> GetAllUniqueWords()
    {
        return UniqueWords.ToList();
    }
}