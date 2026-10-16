using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Common.Services;

public class StaticSuggestedApplicationSource(IReadOnlyCollection<SuggestedApplicationCandidate> candidates) : ISuggestedApplicationSource
{
    public Task<IReadOnlyCollection<SuggestedApplicationCandidate>> GetCandidatesAsync()
    {
        return Task.FromResult(candidates);
    }
}