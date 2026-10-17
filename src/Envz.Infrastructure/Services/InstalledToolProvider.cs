using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Infrastructure.Services;

public class InstalledToolProvider(IEnumerable<ISuggestedToolSource> sources) : IInstalledToolProvider
{
    public async Task<IReadOnlyCollection<Tool>> GetInstalledAsync()
    {
        IReadOnlyCollection<SuggestedToolCandidate>[] results = await Task.WhenAll(sources.Select(s => s.GetCandidatesAsync()));
        return results
            .SelectMany(candidates => candidates)
            .Select(c => new SuggestedToolCandidate(c.Name, Environment.ExpandEnvironmentVariables(c.Path)))
            .Where(c => File.Exists(c.Path))
            .DistinctBy(c => c.Path, StringComparer.OrdinalIgnoreCase)
            .DistinctBy(c => c.Name)
            .Select(c => new Tool(c.Name, c.Path))
            .ToList();
    }
}