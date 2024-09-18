using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;

class Program
{
    static void Main()
    {
        string document1 = "amazing book I think it have a great balance and hope it is a beautiful read indeed it is a masterpiece and a book to be read because poetry have its own flow";
        string document2 = "this will be great i really want an e-reader but love my physical books so much that i do not see the point of buy one because they have beautiful covers but if i can enjoy some of the benefits of an e-reader on the go then come home and pick up the physical copy i will do it";
        
        List<string> docList = new List<string>{document1, document2};

        //Make the string a list of words
        List<string> string_to_list = OrganizeWords.String_to_List(docList);
        /*foreach (string word in string_to_list)
        {
            Console.WriteLine(word);
        }*/

        List<List<string>> listOfWordLists = OrganizeWords.CreateWordLists(docList);
        /*Console.WriteLine("Lists of Words:");
        foreach (var wordList in listOfWordLists)
        {
            Console.WriteLine("Document:");
            foreach (string word in wordList)
            {
                Console.WriteLine(word);
            }
            Console.WriteLine();
        }*/

        // Get the unique words from all documents
        List<string> uniqueWords = OrganizeWords.GetUniqueWords(string_to_list);

        // Output the list
        /*foreach (string word in uniqueWords)
        {
            Console.WriteLine(word);
        }*/


        List<Dictionary<string, int>> wordOccurrences = OrganizeWords.CountWordOccurrences(listOfWordLists, uniqueWords);
        
        /*Console.WriteLine("\nWord Occurrences:");
        for (int i = 0; i < wordOccurrences.Count; i++)
        {
            Console.WriteLine($"Document {i + 1}:");
            foreach (var entry in wordOccurrences[i])
            {
                Console.WriteLine($"{entry.Key}: {entry.Value}");
            }
            Console.WriteLine();
        }*/

        List<Dictionary<string, double>> tfList = TF_IDF.ComputeTF(wordOccurrences);
        /*Console.WriteLine("Get the TF:");
        Console.WriteLine();
        int i = 0;
        foreach (var tfDict in tfList)
        {
            i++;
            Console.WriteLine($"Document {i}");
            foreach (var entry in tfDict)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:G}");
            }
            Console.WriteLine();
        }*/

        Dictionary<string, int> documentFrequency = OrganizeWords.ComputeDocumentFrequency(wordOccurrences);
    
        // Output the document frequencies
        /*foreach (var entry in documentFrequency)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }*/

        Dictionary<string, double> idfList = TF_IDF.ComputeIDF(wordOccurrences, documentFrequency);

        // Output the IDF values
        /*Console.WriteLine("IDF Values:");
        foreach (var entry in idfList)
        {
            Console.WriteLine($"{entry.Key}: {entry.Value:G}");
        }*/

        // Compute TF-IDF
        List<Dictionary<string, double>> tf_idfList = TF_IDF.ComputeTF_IDF(tfList, idfList);

        // Output TF-IDF values
        /*int docIndex = 1;
        foreach (var tf_idfDict in tf_idfList)
        {
            Console.WriteLine($"Document {docIndex} TF-IDF:");
            foreach (var entry in tf_idfDict)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value:G}");
            }
            Console.WriteLine();
            docIndex++;
        }*/
    }
}
