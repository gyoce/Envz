using Envz.Infrastructure.Configuration.Dtos;

namespace Envz.Infrastructure.Configuration.Stores;

public interface IIconStore
{
    IconsDto Icons { get; }

    void Save();
}

public class IconStore(IFileSystem fileSystem, IConfigurationFilesPathProvider filesPathProvider)
    : JsonFileStore<IconsDto>(fileSystem), IIconStore
{
    protected override string FilePath => filesPathProvider.IconsFilePath;

    public IconsDto Icons => Data;
}