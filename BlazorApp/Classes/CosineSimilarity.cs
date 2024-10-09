using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class CosineSimilarity
{
    private Dictionary<string, Dictionary<string, double>> documentVectors;

    public CosineSimilarity(string jsonFilePath)
    {
        LoadVectorsFromJson(jsonFilePath);
    }

    private void LoadVectorsFromJson(string jsonFilePath)
    {
        string jsonContent = File.ReadAllText(jsonFilePath);
        documentVectors = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, double>>>(jsonContent);
    }

    public Dictionary<string, double> CalculateSimilarities(string query)
    {
        Dictionary<string, double> queryVector = CreateQueryVector(query);
        Dictionary<string, double> similarities = new Dictionary<string, double>();

        foreach (var docPair in documentVectors)
        {
            string docName = docPair.Key;
            Dictionary<string, double> docVector = docPair.Value;

            double similarity = CalculateCosineSimilarity(queryVector, docVector);
            similarities[docName] = similarity;
        }

        return similarities.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }

    private Dictionary<string, double> CreateQueryVector(string query)
    {
        string[] queryTerms = query.ToLower().Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        Dictionary<string, double> queryVector = new Dictionary<string, double>();

        foreach (string term in queryTerms)
        {
            if (!queryVector.ContainsKey(term))
            {
                queryVector[term] = 1.0;
            }
            else
            {
                queryVector[term] += 1.0;
            }
        }

        // Normalize query vector
        double magnitude = Math.Sqrt(queryVector.Values.Sum(x => x * x));
        foreach (string term in queryVector.Keys.ToList())
        {
            queryVector[term] /= magnitude;
        }

        return queryVector;
    }

    private double CalculateCosineSimilarity(Dictionary<string, double> queryVector, Dictionary<string, double> docVector)
    {
        double dotProduct = 0;
        double queryMagnitude = 0;
        double docMagnitude = 0;

        foreach (var term in queryVector.Keys)
        {
            if (docVector.ContainsKey(term))
            {
                dotProduct += queryVector[term] * docVector[term];
            }
            queryMagnitude += queryVector[term] * queryVector[term];
        }

        foreach (var value in docVector.Values)
        {
            docMagnitude += value * value;
        }

        queryMagnitude = Math.Sqrt(queryMagnitude);
        docMagnitude = Math.Sqrt(docMagnitude);

        if (queryMagnitude == 0 || docMagnitude == 0)
        {
            return 0;
        }

        return dotProduct / (queryMagnitude * docMagnitude);
    }
}