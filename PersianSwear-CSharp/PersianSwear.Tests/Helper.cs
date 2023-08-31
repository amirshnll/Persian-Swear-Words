using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace PersianSwear.Tests;

public class Helper : ConfigLoader
{
    private static string _absolutePath = Path.Combine(Environment.CurrentDirectory, "data.json");
    private readonly Words _words;

    public string GenerateSensitivePhrase()
    {
        return _words.SensitivePhrases[Random.Shared.Next(0, _words.SensitivePhrases.Count - 1)];
    }
    public string GenerateSensitiveFuzzyPhrase()
    {
        //TODO: Add random char generator
        return "الق"+_words.SensitivePhrases[Random.Shared.Next(0, _words.SensitivePhrases.Count - 1)]  + "ضصث";
    }

    public string GenerateInSensitivePhrase()
    {
        return "سلام";
    }

    public string GenerateSensitiveSentence()
    {
        StringBuilder builder = new();
        for (var i = 0; i < 10; i++)
        {
            builder.Append(GenerateSensitivePhrase() + " ");
            builder.Append(GenerateInSensitivePhrase() + " ");
            builder.AppendLine();
        }

        return builder.ToString();
    }

    public string GenerateInSensitiveSentence()
    {
        StringBuilder builder = new();
        for (var i = 0; i < 10; i++)
        {
            builder.Append(GenerateInSensitivePhrase() + " ");
            builder.AppendLine();
        }

        return builder.ToString();
    }

    public bool DoesPhraseExistInFile(string phrase)
    {
        return _words.SensitivePhrases.Any(x => x == phrase);
    }
    public Helper(string? customAbsolutePath = "")
    {
        if (!string.IsNullOrEmpty(customAbsolutePath))
            _absolutePath = customAbsolutePath;

        var readFile = File.ReadAllText(_absolutePath);
        var deserialize = JsonSerializer.Deserialize<Words>(readFile);
        _words = deserialize ?? throw new ArgumentException($"{nameof(deserialize)} can't parse json object.");
    }
}