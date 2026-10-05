using Envz.Domain.Entities;
using Envz.Domain.Exceptions;

namespace Envz.Infrastructure.Configuration.Dtos;

public static class DtoExtensionMethods
{
    public static Application ToEntity(this ApplicationDto dto, Dictionary<string, string> icons)
    {
        try
        {
            return new Application(
                dto.Name,
                dto.Path,
                icons.TryGetValue(dto.Name, out string? icon) && !string.IsNullOrWhiteSpace(icon) ? Convert.FromBase64String(icon) : null
            );
        }
        catch (ValidationException ex)
        {
            throw new InvalidConfigurationException($"Application \"{dto.Name}\": {ex.Message}");
        }
        catch (FormatException)
        {
            throw new InvalidConfigurationException($"Application \"{dto.Name}\": icon is not valid base64");
        }
    }

    public static Environment ToEntity(this EnvironmentDto dto)
    {
        try
        {
            return new Environment(
                dto.Name,
                dto.Applications.Select(envAppDto => new EnvironmentApplication(envAppDto.ApplicationName, envAppDto.Parameter))
            );
        }
        catch (ValidationException ex)
        {
            throw new InvalidConfigurationException($"Environment \"{dto.Name}\": {ex.Message}");
        }
    }
}