using EnvBooster.Domain.Ports;

namespace EnvBooster.Application.Environments;

public class GetEnvironmentsUseCase(IEnvironmentRepository environmentRepository)
{
    public IReadOnlyCollection<Environment> Execute()
    {
        return environmentRepository.GetAll();
    }
}