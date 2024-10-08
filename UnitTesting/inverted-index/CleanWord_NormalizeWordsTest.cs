using System;
using System.Reflection;
using Xunit;

public class CleanWord_NormalizeWordsTest
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Running CleanWord_NormalizeWords test:");
        var test = new CleanWord_NormalizeWordsTest();
        test.TestCleanWord_NormalizeWords("  Hello  ", "hello");
        test.TestCleanWord_NormalizeWords("WORLD", "world");
        test.TestCleanWord_NormalizeWords("TeSt", "test");
        Console.WriteLine("Test completed.");
    }

    [Theory]
    [InlineData("  Hello  ", "hello")]
    [InlineData("WORLD", "world")]
    [InlineData("TeSt", "test")]
    public void TestCleanWord_NormalizeWords(string input, string expected)
    {
        // Arrange
        var invertedIndex = new InvertedIndex();
        var cleanWordMethod = typeof(InvertedIndex).GetMethod("CleanWord", BindingFlags.NonPublic | BindingFlags.Instance);

        // Act
        string result = (string)cleanWordMethod.Invoke(invertedIndex, new object[] { input });

        // Assert
        Assert.Equal(expected, result);

        // Console output for manual verification
        Console.WriteLine($"Input: '{input}', Cleaned: '{result}'");
    }
}
