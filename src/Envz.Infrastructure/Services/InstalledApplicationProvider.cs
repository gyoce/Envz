using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Infrastructure.Services;

public class InstalledApplicationProvider(IEnumerable<ISuggestedApplicationSource> sources) : IInstalledApplicationProvider
{
    public async Task<IReadOnlyCollection<Application>> GetInstalledAsync()
    {
        IReadOnlyCollection<SuggestedApplicationCandidate>[] results = await Task.WhenAll(sources.Select(s => s.GetCandidatesAsync()));
        return results
            .SelectMany(candidates => candidates)
            .Select(c => new SuggestedApplicationCandidate(c.Name, System.Environment.ExpandEnvironmentVariables(c.Path)))
            .Where(c => File.Exists(c.Path))
            .DistinctBy(c => c.Path, StringComparer.OrdinalIgnoreCase)
            .DistinctBy(c => c.Name)
            .Select(c => new Application(c.Name, c.Path))
            .ToList();
    }
}