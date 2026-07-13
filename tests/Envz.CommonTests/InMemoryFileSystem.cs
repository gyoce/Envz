using Envz.Infrastructure.Configuration;

namespace Envz.CommonTests;

public class InMemoryFileSystem : IFileSystem
{
    public Dictionary<string, int> NumberOfCallsExists { get; } = [];

    public Dictionary<string, string> Files { get; } = [];

    public bool Exists(string path)
    {
        if (!NumberOfCallsExists.TryAdd(path, 1))
            NumberOfCallsExists[path]++;
        return Files.ContainsKey(path);
    }

    public string ReadAllText(string path) => Files[path];
    public void WriteAllText(string path, string content) => Files[path] = content;
    public void CreateDirectory(string path) { }
}
