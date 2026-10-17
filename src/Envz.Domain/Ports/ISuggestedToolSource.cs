using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface ISuggestedToolSource
{
    Task<IReadOnlyCollection<SuggestedToolCandidate>> GetCandidatesAsync();
}