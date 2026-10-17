using Envz.Application.Mediator;
using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Application.Requests.Tools;

public record GetSuggestedToolsRequest : IRequest<IReadOnlyCollection<SuggestedTool>>;

public class GetSuggestedToolsUseCase(IInstalledToolProvider installedToolProvider, IToolRepository toolRepository)
    : IAsyncUseCase<GetSuggestedToolsRequest, IReadOnlyCollection<SuggestedTool>>
{
    public async Task<IReadOnlyCollection<SuggestedTool>> ExecuteAsync(GetSuggestedToolsRequest request)
    {
        IReadOnlyCollection<Tool> existing = toolRepository.GetAll();

        return (await installedToolProvider.GetInstalledAsync())
            .Select(app => new SuggestedTool(app, IsAlreadyAdded(app, existing)))
            .OrderBy(suggestion => suggestion.IsAlreadyAdded)
            .ThenBy(suggestion => suggestion.Tool.Name, StringComparer.CurrentCultureIgnoreCase)
            .ToList();
    }

    private static bool IsAlreadyAdded(Tool app, IReadOnlyCollection<Tool> existing)
    {
        return existing.Any(tool => tool.Name == app.Name || string.Equals(tool.Path, app.Path, StringComparison.OrdinalIgnoreCase));
    }
}