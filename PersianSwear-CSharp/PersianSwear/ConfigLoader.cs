using System.Text.Json;

namespace PersianSwear;

public abstract class ConfigLoader
{
    private static string _absolutePath = Path.Combine(Environment.CurrentDirectory, "data.json");
    protected readonly Words Words;

    /// <summary>
    /// Create New Filter For Persian Words
    /// </summary>
    /// <param name="customAbsolutePath">Optional Custom Path For Json File</param>
    /// <exception cref="ArgumentException">If Json File Is Corrupted This Exception Will Throw</exception>
    public ConfigLoader(string? customAbsolutePath = "")
    {
        if (!string.IsNullOrEmpty(customAbsolutePath))
            _absolutePath = customAbsolutePath;

        var readFile = File.ReadAllText(_absolutePath);
        var deserialize = JsonSerializer.Deserialize<Words>(readFile);
        Words = deserialize ?? throw new ArgumentException($"{nameof(deserialize)} can't parse json object.");
    }
}