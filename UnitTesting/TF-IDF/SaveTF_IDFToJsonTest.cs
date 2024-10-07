using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

class SaveTF_IDFToJsonTest
{
    static void Main()
    {
        var tf_idf = new Dictionary<string, Dictionary<string, double>>
        {
            ["doc1"] = new Dictionary<string, double> { ["apple"] = 0, ["banana"] = 0.17328679513998632, ["cherry"] = 0 },
            ["doc2"] = new Dictionary<string, double> { ["apple"] = 0, ["cherry"] = 0 }
        };

        TF_IDF.tf_idf tfIdf = new TF_IDF.tf_idf();
        tfIdf.SaveTF_IDFToJson(tf_idf, "testFolder");

        string filePath = Path.Combine("TF_IDF", "testFolder_tf_idf.json");
        
        // Assertions
        Assert.True(File.Exists(filePath));

        string jsonContent = File.ReadAllText(filePath);
        Assert.Contains("\"doc1\"", jsonContent);
        Assert.Contains("\"doc2\"", jsonContent);
        Assert.Contains("\"apple\"", jsonContent);
        Assert.Contains("\"banana\"", jsonContent);
        Assert.Contains("\"cherry\"", jsonContent);

        Console.WriteLine($"File created successfully at: {filePath}");
        Console.WriteLine("File content:");
        Console.WriteLine(jsonContent);
        Console.WriteLine("All tests passed!");
    }
}
