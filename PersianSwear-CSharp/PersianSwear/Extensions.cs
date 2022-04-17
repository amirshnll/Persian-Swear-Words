using PersianSwear.Internal;

namespace PersianSwear;

public static class Extensions
{
    /// <summary>
    /// check if the given word is sensitive,
    /// </summary>
    /// <param name="word">Incoming Word To Check</param>
    /// <returns>Return <c>true</c> if word is offensive otherwise return <c>false</c> </returns> 
    public static bool IsBadWord(this string word)
    {
        return Vars.Words.WordCollection.Any(p => p == word || p.Contains(word) || p.StartsWith(word) || p.EndsWith(word));
    }
    /// <summary>
    /// Check If There Is bad word Inside the Sentence
    /// </summary>
    /// <param name="sentence">The Incoming Sentence/Comment To Check</param>
    /// <param name="expectCount">How Many Bad Words Are Expected If Founded Word's Count In Sentence Are Than <c>expectCount</c> Method Will Return True</param>
    /// <returns>Return True If Bad Words Count Is More Than <c>expectCount</c></returns>
    public static bool IsBadSentence(this string sentence, ushort expectCount = 1)
    {
        var inputWordList = sentence.Split(' ');

        var counter = inputWordList.Count(word => word.IsBadWord());
        return counter >= expectCount;
    }
    /// <summary>
    /// Remove All The Bad Words From The Sentence.
    /// </summary>
    /// <param name="sentence">InComing Sentence/Comment To Check</param>
    /// <returns>Return Filtered Text.</returns>
    public static string RemoveBadWords(this string sentence)
    {
        var words = sentence.Split(' ');

        return words
            .Where(word => !word.IsBadWord())
            .Aggregate("", (current, word) => current + word);
    }
    /// <summary>
    /// Find All Bad Worlds Inside A Sentence.
    /// </summary>
    /// <param name="sentence">Incoming sentence/Comment To Check</param>
    /// <returns>Return <c>List</c> of bad Worlds Exists In Given Sentence.</returns>
    public static List<string> GetBadWords(this string sentence)
    {
        return sentence.Split(' ').Where(word => word.IsBadWord()).ToList();
    }
}