using System;
using System.Collections.Generic;
using Xunit;

class String_to_ListTest
{
    static void Main()
    {
        var documents = new Dictionary<string, string>
        {
            ["doc1"] = "This is a test.",
            ["doc2"] = "Another test document."
        };

        var result = OrganizeWords.String_to_List(documents);

        Console.WriteLine("String_to_List Result:");
        foreach (var word in result)
        {
            Console.WriteLine(word);
        }
    }
}
