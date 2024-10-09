using System.Collections.Generic;

public interface ISimilarity
{
    Dictionary<string, double> CalculateSimilarities(string query);
}

public abstract class Similarity : ISimilarity
{
    protected Dictionary<string, Dictionary<string, double>> documentVectors;

    protected Similarity(string jsonFilePath)
    {
        LoadVectorsFromJson(jsonFilePath);
    }

    protected abstract void LoadVectorsFromJson(string jsonFilePath);

    public abstract Dictionary<string, double> CalculateSimilarities(string query);

    protected abstract Dictionary<string, double> CreateQueryVector(string query);
}
