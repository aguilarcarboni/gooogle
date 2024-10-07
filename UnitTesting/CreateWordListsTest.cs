using System;
using System.Collections.Generic;
using Xunit;

class CreateWordListsTest
{
    static void Main()
    {
        var documents = new Dictionary<string, string>
        {
            ["test1"] = "This is a test.",
            ["test2"] = "Another test document."
        };

        var result = OrganizeWords.CreateWordLists(documents);

        Console.WriteLine("CreateWordLists Result:");
        foreach (var doc in result)
        {
            Console.WriteLine($"{doc.Key}: {string.Join(", ", doc.Value)}");
        }
    }
}
