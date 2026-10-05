using Envz.Domain.Exceptions;
using System.Text.Json;

namespace Envz.Infrastructure.Configuration.Stores;

public abstract class JsonFileStore<TDto>(IFileSystem fileSystem)
    where TDto : class, new()
{
    protected abstract string FilePath { get; }
    private static readonly JsonSerializerOptions SerializerOptions = new() { WriteIndented = true, IndentSize = 2 };

    protected TDto Data
    {
        get => field ??= Load();
    }

    public void Save()
    {
        string? directory = Path.GetDirectoryName(FilePath);
        if (!string.IsNullOrEmpty(directory))
            fileSystem.CreateDirectory(directory);

        string json = JsonSerializer.Serialize(Data, SerializerOptions);
        fileSystem.WriteAllText(FilePath, json);
    }

    private TDto Load()
    {
        if (!fileSystem.Exists(FilePath))
            return new TDto();

        try
        {
            string json = fileSystem.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<TDto>(json, SerializerOptions) ?? new TDto();
        }
        catch (JsonException ex)
        {
            throw new InvalidConfigurationException($"{FilePath}: {ex.Message}");
        }
    }
}