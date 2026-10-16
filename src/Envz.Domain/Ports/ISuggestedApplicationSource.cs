using Envz.Domain.Entities;

namespace Envz.Domain.Ports;

public interface ISuggestedApplicationSource
{
    Task<IReadOnlyCollection<SuggestedApplicationCandidate>> GetCandidatesAsync();
}