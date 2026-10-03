using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Settings;

public record UpdateLanguageRequest : IRequest
{
    public string Language { get; set; } = string.Empty;
}

public class UpdateLanguageUseCase(ISettingsRepository settingsRepository) : IUseCase<UpdateLanguageRequest>
{
    public void Execute(UpdateLanguageRequest request)
    {
        UserSettings settings = settingsRepository.Get();
        settings.Language = request.Language;
        settingsRepository.Save(settings);
    }
}