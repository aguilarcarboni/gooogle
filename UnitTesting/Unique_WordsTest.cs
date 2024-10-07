using System;
using System.Collections.Generic;
using Xunit;

class Unique_WordsTest
{
    static void Main()
    {
        var words = new List<string> { "apple", "banana", "apple", "cherry", "banana" };

        var result = OrganizeWords.Unique_Words(words);

        Console.WriteLine("Unique_Words Result:");
        foreach (var word in result)
        {
            Console.WriteLine(word);
        }
    }
}
