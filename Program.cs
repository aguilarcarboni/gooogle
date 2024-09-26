using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;

class Program
{
    static void Main()
    {
        Drive drive = new Drive();
        Document document = new Document("text", "document1.txt");
        drive.AddDocument(document);

        foreach (Document doc in drive.GetDocuments())
        {
            foreach (string word in doc.GetUniqueWords())
            {
                Console.WriteLine(word);
            }
        }
    }
}
