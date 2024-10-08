using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

public class ComputeTF_IDFTest
{
    private readonly ITestOutputHelper _output;
    private readonly TF_IDF.tf_idf _tfIdf;

    public ComputeTF_IDFTest(ITestOutputHelper output)
    {
        _output = output;
        _tfIdf = new TF_IDF.tf_idf();
    }

    [Fact]
    public void TestComputeTF_IDF()
    {
        _output.WriteLine("Running ComputeTF_IDF test:");

        // Arrange
        var tf = new Dictionary<string, Dictionary<string, double>>
        {
            {"doc1", new Dictionary<string, double> {{"hello", 2.0/3}, {"world", 1.0/3}}},
            {"doc2", new Dictionary<string, double> {{"hello", 1.0/3}, {"test", 2.0/3}}}
        };
        var idf = new Dictionary<string, double>
        {
            {"hello", 0},
            {"world", Math.Log(2)},
            {"test", Math.Log(2)}
        };

        // Act
        var result = _tfIdf.ComputeTF_IDF(tf, idf);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(0, result["doc1"]["hello"], 3);
        Assert.Equal(Math.Log(2)/3, result["doc1"]["world"], 3);
        Assert.Equal(0, result["doc1"]["test"], 3);
        Assert.Equal(0, result["doc2"]["hello"], 3);
        Assert.Equal(0, result["doc2"]["world"], 3);
        Assert.Equal(2*Math.Log(2)/3, result["doc2"]["test"], 3);

        // Log output for verification
        _output.WriteLine("TF-IDF results:");
        foreach (var doc in result)
        {
            _output.WriteLine($"Document: {doc.Key}");
            foreach (var word in doc.Value)
            {
                _output.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
        _output.WriteLine("Test completed.");
    }
}
