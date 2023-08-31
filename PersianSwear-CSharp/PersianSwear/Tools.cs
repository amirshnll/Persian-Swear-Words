using System.Text.RegularExpressions;

namespace PersianSwear;

public static class Tools
{
    private const char Separator = ' ';
    private const string Pattern = "\r\n|\r|\n";

    public static IEnumerable<string> GetPhrasesInSentence(this string sentence)
    {
        return sentence.Split(Separator);
    }

    public static string[] GetLinesInSentence(this string sentence)
    {
        return Regex.Split(sentence, Pattern);
    }
}