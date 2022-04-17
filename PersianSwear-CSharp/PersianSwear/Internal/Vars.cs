namespace PersianSwear.Internal;

internal static class Vars
{
    internal static string Path = $"{Environment.CurrentDirectory}\\data.json";
    internal static Words Words = new(Path);
}