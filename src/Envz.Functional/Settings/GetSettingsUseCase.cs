using Envz.Domain.Entities;
using Envz.Domain.Ports;
using Envz.Functional.Mediator;

namespace Envz.Functional.Settings;

public record GetSettingsRequest : IRequest<UserSettings>;

public class GetSettingsUseCase(ISettingsRepository settingsRepository) : IUseCase<GetSettingsRequest, UserSettings>
{
    public UserSettings Execute(GetSettingsRequest request) => settingsRepository.Get();
}