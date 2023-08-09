using FuzzySharp;

namespace PersianSwear;

public class FuzzyFilterPersianWords : ConfigLoader
{
    public FuzzyFilterPersianWords(string? customAbsolutePath = "") : base(customAbsolutePath)
    {
    }

    private int GetRatio(string phrase, string sensitivePhrase)
    {
        var ratio = Fuzz.Ratio(phrase, sensitivePhrase);
        return ratio;
    }

    /// <summary>
    /// Checks if the given word is sensitive,
    /// </summary>
    /// <param name="phrase">Phrase to check.</param>
    /// <param name="precision">Amount of fuzzy search precision</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>true</c> if phrase is sensitive otherwise returns <c>false</c> </returns> 
    public bool IsSensitivePhrase(string phrase, int precision = 75)
    {
        return phrase switch
        {
            null => throw new ArgumentNullException($"{nameof(phrase)} can't be null."),
            "" or " " => false,
            _ => Words.SensitivePhrases.Any(p => GetRatio(p, phrase) > precision)
        };
    }


    /// <summary>
    /// Checks if the given sentence contains any sensitive phrases.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="expectCount">Expected number
    /// sensitive phrases.</param>
    ///<param name="precision">Amount of fuzzy search precision</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns true if  number of sensitive phrases is above <c>expectCount</c></returns>
    public bool IsSensitiveSentence(string sentence, ushort expectCount = 0, int precision = 75)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var inputWordList = sentence.GetPhrasesInSentence();
        var counter = inputWordList.Count(phrase => IsSensitivePhrase(phrase, precision));
        return counter > expectCount;
    }


    /// <summary>
    /// Removes all sensitive phrases from the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="precision">Amount of fuzzy search precision</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns filtered sentence.</returns>
    public string RemoveSensitivePhrases(string sentence, int precision = 75)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();


        foreach (var (value, index) in splitLines.Select((value, index) => (value, index)))
        {
            splitLines[index] = string.Join(" ",
                value.GetPhrasesInSentence().Where(phrase => !IsSensitivePhrase(phrase, precision)));
        }


        return splitLines.Aggregate("", (current, splitLine) => current + ("\n" + splitLine)).TrimStart();
    }

    /// <summary>
    /// Finds sensitive phrases in the given sentence.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="precision">Amount of fuzzy search precision</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    public IEnumerable<string> GetSensitivePhrases(string sentence, int precision = 75)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new List<string>();

        var processedPhrases = new List<string>();
        foreach (var line in splitLines)
        {
            var sensitivePhrases =
                line.GetPhrasesInSentence().Where(phrase => !processedPhrases.Contains(phrase)).Where(phrase =>
                    IsSensitivePhrase(phrase, precision)).ToList();

            processedPhrases.AddRange(sensitivePhrases);
            result.AddRange(sensitivePhrases);
        }

        return result;
    }

    /// <summary>
    /// Finds sensitive phrases and configured phrases that caused the phrases to be sensitive.
    /// </summary>
    /// <param name="sentence">Sentence to check.</param>
    /// <param name="precision">Amount of fuzzy search precision</param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <returns>Returns <c>List</c> of sensitive phrases in the given sentence.</returns>
    public Dictionary<string, List<string>> GetSensitivePhrasesWithMatches(string sentence, int precision = 75)
    {
        if (string.IsNullOrEmpty(sentence))
            throw new ArgumentNullException($"{nameof(sentence)} can't be null.");

        var splitLines = sentence.GetLinesInSentence();

        var result = new Dictionary<string, List<string>>();

        foreach (var lines in splitLines)
        foreach (var phrase in lines.GetPhrasesInSentence())
        {
            if (result.ContainsKey(phrase))
            {
                continue;
            }

            var sensitivePhrases = Words.SensitivePhrases
                .Where(sensitivePhrase => GetRatio(phrase, sensitivePhrase) > precision)
                .ToList();
            if (sensitivePhrases.Any())
            {
                result.Add(phrase, sensitivePhrases);
            }
        }

        return result;
    }
}