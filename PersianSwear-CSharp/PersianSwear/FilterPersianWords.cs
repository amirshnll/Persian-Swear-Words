﻿using System.Text.Json.Serialization;

namespace PersianSwear;

public record Words([property: JsonPropertyName("word")] IReadOnlyList<string> SensitivePhrases);

public class FilterPersianWords : ConfigLoader
{
    public FilterPersianWords(string? customAbsolutePath = "") : base(customAbsolutePath)
    {
    }

    private bool AreEqual(string phrase, string sensitivePhrase)
    {
        return phrase.Contains(sensitivePhrase);
    }

    /// <summary>
    /// Checks if the given phrase is sensitive,
    /// </summary>
    /// <param name="phrase">Phrase to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>true</c> if phrase is sensitive otherwise returns <c>false</c> </returns> 
    public bool IsSensitivePhrase(string phrase)
    {
        return phrase switch
        {
            null => throw new ArgumentNullException($"{nameof(phrase)} can't be null."),
            "" or " " => false,
            _ => Words.SensitivePhrases.Any(p => AreEqual(phrase, p)
            )
        };
    }

    /// <summary>
    /// Checks if the given sentence contains any sensitive phrases.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="expectCount">Expected number
    /// sensitive phrases.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns true if  number of sensitive phrases is above <c>expectCount</c></returns>
    public bool IsSensitiveSentence(string sentence, ushort expectCount = 0)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var inputWordList = sentence.GetPhrasesInSentence();
        var counter = inputWordList.Count(IsSensitivePhrase);
        return counter > expectCount;
    }

    /// <summary>
    /// Removes all sensitive phrases from the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns filtered sentence.</returns>
    public string RemoveSensitivePhrases(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();


        foreach (var (value, index) in splitLines.Select((value, index) => (value, index)))
        {
            splitLines[index] = string.Join(" ", value.GetPhrasesInSentence().Except(Words.SensitivePhrases));
        }

        return splitLines.Aggregate("", (current, splitLine) => current + ("\n" + splitLine)).TrimStart();
    }

    /// <summary>
    /// Finds sensitive phrases in the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    public IEnumerable<string> GetSensitivePhrases(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new List<string>();

        foreach (var lines in splitLines)
            result.AddRange(lines.GetPhrasesInSentence().Where(IsSensitivePhrase).ToList());

        return result;
    }

    /// <summary>
    /// Finds sensitive phrases and configured phrases that caused the phrases to be sensitive.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    public Dictionary<string, List<string>> GetSensitivePhrasesWithMatches(string sentence)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new Dictionary<string, List<string>>();

        foreach (var line in splitLines)
        foreach (var phrase in line.GetPhrasesInSentence())
        {
            if (result.ContainsKey(phrase))
            {
                continue;
            }

            var sensitivePhrases = Words.SensitivePhrases.Where(sensitivePhrase => AreEqual(phrase, sensitivePhrase))
                .ToList();
            if (sensitivePhrases.Any())
            {
                result.Add(phrase, sensitivePhrases);
            }
        }

        return result;
    }
}