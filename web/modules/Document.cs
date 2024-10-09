public class Document
{
    public string Type { get; set; }
    public string Content { get; set; }
    public string FileName { get; set; }

    public Document(string type, string fileName)
    {
        Type = type;
        FileName = fileName;
        Content = LoadContent(fileName);

    }

    public List<string> GetUniqueWords() {
        if (string.IsNullOrEmpty(Content))
        {
            return new List<string>();
        }

        // Use ContentToList to get all words
        var allWords = ContentToList();

        // Use a HashSet to eliminate duplicates and convert to lowercase
        var uniqueWords = new HashSet<string>(allWords.Select(w => w.ToLower()));

        // Convert HashSet back to List and return
        return uniqueWords.ToList();
    }

    public List<string> ContentToList(params char[] delimiters) {
        if (string.IsNullOrEmpty(Content))
        {
            return new List<string>();
        }

        delimiters = delimiters.Length > 0 ? delimiters : new[] { ',', ';', ' ' };
        return Content.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public bool IsValid() {
        return !string.IsNullOrEmpty(Type) && !string.IsNullOrEmpty(Content);
    }

    private string LoadContent(string fileName) {
        
        var path = Path.Combine("drive", fileName);

        if (File.Exists(path)) {
            string content = File.ReadAllText(path);
            Console.WriteLine($"File content loaded from: {path}");
            return content;
        }
        else {
            Console.WriteLine($"File not found: {path}");
            Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
            return "";
        }
    }
}