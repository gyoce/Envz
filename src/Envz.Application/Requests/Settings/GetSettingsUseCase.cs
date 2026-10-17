using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Settings;

public record GetSettingsRequest : IRequest<UserSettings>;

public class GetSettingsUseCase(ISettingsRepository settingsRepository) : IUseCase<GetSettingsRequest, UserSettings>
{
    public UserSettings Execute(GetSettingsRequest request) => settingsRepository.Get();
}