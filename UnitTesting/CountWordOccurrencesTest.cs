using System;
using System.Collections.Generic;
using Xunit;

class CountWordOccurrencesTest
{
    static void Main()
    {
        var docs = new Dictionary<string, List<string>>
        {
            ["test1"] = new List<string> { "apple", "banana", "apple" },
            ["test2"] = new List<string> { "apple", "cherry", "cherry" }
        };

        var uniqueWords = new List<string> { "apple", "banana", "cherry" };

        var result = OrganizeWords.CountWordOccurrences(docs, uniqueWords);

        Console.WriteLine("CountWordOccurrences Result:");
        foreach (var doc in result)
        {
            Console.WriteLine($"{doc.Key}:");
            foreach (var word in doc.Value)
            {
                Console.WriteLine($"  {word.Key}: {word.Value}");
            }
        }
    }
}
