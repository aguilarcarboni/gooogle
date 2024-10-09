using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class EuclideanSimilarity
{
    private Dictionary<string, Dictionary<string, double>> documentVectors;

    public EuclideanSimilarity(string jsonFilePath)
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

            double distance = CalculateEuclideanDistance(queryVector, docVector);
            double similarity = 1 / (1 + distance); // Convert distance to similarity
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

        return queryVector;
    }

    private double CalculateEuclideanDistance(Dictionary<string, double> queryVector, Dictionary<string, double> docVector)
    {
        double sumSquaredDifferences = 0;

        // Calculate differences for terms in both vectors
        foreach (var term in queryVector.Keys.Union(docVector.Keys))
        {
            double queryValue = queryVector.ContainsKey(term) ? queryVector[term] : 0;
            double docValue = docVector.ContainsKey(term) ? docVector[term] : 0;
            double difference = queryValue - docValue;
            sumSquaredDifferences += difference * difference;
        }

        return Math.Sqrt(sumSquaredDifferences);
    }
}