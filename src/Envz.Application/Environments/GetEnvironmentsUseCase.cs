using Envz.Domain.Ports;

namespace Envz.Application.Environments;

public class GetEnvironmentsUseCase(IEnvironmentRepository environmentRepository)
{
    public IReadOnlyCollection<Environment> Execute()
    {
        return environmentRepository.GetAll();
    }
}