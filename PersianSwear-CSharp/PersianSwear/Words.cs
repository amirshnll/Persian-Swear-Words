using System.Text.Json;

namespace PersianSwear;

public class Words
{
    public Words(string dataPath)
    {
        var findFile = new FileInfo(dataPath);
        var readFile = File.ReadAllText(findFile.FullName);
        WordCollection = JsonSerializer.Deserialize<List<string>>(readFile) ?? throw new ArgumentNullException($"{nameof(dataPath)}. File Not Found!");
    }
    public IReadOnlyCollection<string> WordCollection { get; }
}