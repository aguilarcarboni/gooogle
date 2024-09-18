using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Linq;

static class TF_IDF {
    public static List<Dictionary<string, double>> ComputeTF(List<Dictionary<string, int>> WordOccurrences) {

        // Initialize a list to hold TF dictionaries for each document
        List<Dictionary<string, double>> tfList = new List<Dictionary<string, double>>();

        foreach (var wordDict in WordOccurrences) {
            // Initialize a new dictionary to hold TF values for the current document
            Dictionary<string, double> tfDict = new Dictionary<string, double>();

            // Calculate the total number of words in the document
            int totalWords = 0;
            foreach(var val in wordDict) {
                totalWords += val.Value;
            }

            // Calculate TF for each word in the document
            foreach(var val in wordDict) {
                string word = val.Key;
                int count = val.Value;
                double tf = (double)count / totalWords;
                tfDict[word] = tf;
            }

            // Add the TF dictionary for the current document to the list
            tfList.Add(tfDict);
        }

        return tfList;
    }
    public static Dictionary<string, double> ComputeIDF(List<Dictionary<string, int>> wordOccurrences, Dictionary<string, int> documentFrequency) {
        // Initialize a dictionary to store the IDF values for each word
        Dictionary<string, double> idfList = new Dictionary<string, double>();

        // Get the number of documents
        int numberOfDocuments = wordOccurrences.Count;

        // Calculate the IDF for each word
        foreach (var entry in documentFrequency) {
            string word = entry.Key;
            int docFrequency = entry.Value;

            // Compute the IDF value using the formula
            double idf = Math.Log((double)numberOfDocuments / docFrequency);

            // Add the IDF value to the dictionary
            idfList[word] = idf;
        }

        return idfList;
    }
    public static List<Dictionary<string, double>> ComputeTF_IDF(List<Dictionary<string, double>> tf, Dictionary<string, double> idf) {
        // Initialize a list to hold TF-IDF dictionaries for each document
        List<Dictionary<string, double>> tf_idf = new List<Dictionary<string, double>>();

        // Iterate through each TF dictionary (one for each document)
        foreach (var tfDict in tf)
        {
            // Initialize a new dictionary to hold TF-IDF values for the current document
            Dictionary<string, double> tf_idf_Dict = new Dictionary<string, double>();

            // Compute TF-IDF for each word in the current document's TF dictionary
            foreach (var tfEntry in tfDict)
            {
                string word = tfEntry.Key;
                double tfValue = tfEntry.Value;

                // Check if the word's IDF value is available
                if (idf.TryGetValue(word, out double idfValue))
                {
                    // Compute TF-IDF and add it to the dictionary
                    double tf_idf_value = tfValue * idfValue;
                    tf_idf_Dict[word] = tf_idf_value;
                }
            }

            // Add the TF-IDF dictionary for the current document to the list
            tf_idf.Add(tf_idf_Dict);
        }

        return tf_idf;
    }
}