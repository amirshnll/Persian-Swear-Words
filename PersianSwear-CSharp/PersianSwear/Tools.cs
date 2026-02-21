using System.Text.RegularExpressions;

namespace PersianSwear;

public static class Tools
{
	private static readonly char[] Separators = new char[] { ' ', '.', '-', '_', ',', ';' };
	private const string Pattern = "\r\n|\r|\n";

    public static IEnumerable<string> GetPhrasesInSentence(this string sentence)
    {
        return sentence.Split(Separators);
    }

    public static string[] GetLinesInSentence(this string sentence)
    {
        return Regex.Split(sentence, Pattern);
	}

	public static string RemoveLinesInSentence(this string sentence)
	{
		return sentence.Replace("\r", "");
	}

	public static string RemoveSeperatiorsInSentence(this string sentence)
	{
		return sentence.Replace('.', ' ').Replace('-', ' ').Replace('_', ' ').Replace(',', ' ').Replace(';', ' ');
	}
}