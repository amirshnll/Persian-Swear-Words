using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace PersianSwear;

public record Words([property: JsonPropertyName("word")] IReadOnlyList<string> Word);

public class FilterPersianWords
{

    internal static string Path = $"{Environment.CurrentDirectory}\\data.json";
    internal Words Words;
    public FilterPersianWords(string? customPath = "")
    {
        if (!string.IsNullOrEmpty(customPath))
            Path = customPath;

        var readFile = File.ReadAllText(Path);
        var deserialize = JsonSerializer.Deserialize<Words>(readFile);
        Words = deserialize ?? throw new ArgumentException($"{nameof(deserialize)} cant parse json object");
    }

    /// <summary>
    /// check if the given word is sensitive,
    /// </summary>
    /// <param name="word">Incoming Word To Check</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Return <c>true</c> if word is offensive otherwise return <c>false</c> </returns> 
    public bool IsBadWord(string word)
    {
        return word switch
        {
            null => throw new ArgumentNullException($"{nameof(word)} cant be null"),
            "" or " " => false,
            _ => Words.Word.Any(p => p == word || p.Contains(word) || p.StartsWith(word) || p.EndsWith(word))
        };
    }

    /// <summary>
    /// Check If There Is bad word Inside the Sentence
    /// </summary>
    /// <param name="sentence">The Incoming Sentence/Comment To Check</param>
    /// <param name="expectCount">How Many Bad Words Are Expected If Founded Word's Count In Sentence Are Than <c>expectCount</c> Method Will Return True</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Return True If Bad Words Count Is More Than <c>expectCount</c></returns>
    public bool IsBadSentence(string sentence, ushort expectCount = 1)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} cant be null");

        var inputWordList = sentence.Split(' ');
        var counter = inputWordList.Count(IsBadWord);
        return counter >= expectCount;
    }

    /// <summary>
    /// Remove All The Bad Words From The Sentence.
    /// </summary>
    /// <param name="sentence">InComing Sentence/Comment To Check</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Return Filtered Text.</returns>
    public string RemoveBadWords(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} cant be null");

        var splitLines = Regex.Split(sentence, "\r\n|\r|\n");


        foreach (var (value, index) in splitLines.Select((value, index) => (value, index)))
        {
            splitLines[index] = string.Join(" ", value.Split(' ').Except(Words.Word));
        }

        return splitLines.Aggregate("", (current, splitLine) => current + ("\n" + splitLine));
    }

    /// <summary>
    /// Find All Bad Worlds Inside A Sentence.
    /// </summary>
    /// <param name="sentence">Incoming sentence/Comment To Check</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Return <c>List</c> of bad Worlds Exists In Given Sentence.</returns>
    public List<string> GetBadWords(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} cant be null");

        var splitLines = Regex.Split(sentence, "\r\n|\r|\n");

        var result = new List<string>();

        foreach (var lines in splitLines)
            result.AddRange(lines.Split(' ').Where(IsBadWord).ToList());

        return result;
    }
}
