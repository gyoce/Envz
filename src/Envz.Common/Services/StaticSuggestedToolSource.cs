using Envz.Domain.Entities;
using Envz.Domain.Ports;

namespace Envz.Common.Services;

public class StaticSuggestedToolSource(IReadOnlyCollection<SuggestedToolCandidate> candidates) : ISuggestedToolSource
{
    public Task<IReadOnlyCollection<SuggestedToolCandidate>> GetCandidatesAsync()
    {
        return Task.FromResult(candidates);
    }
}