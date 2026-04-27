using EnvBooster.Domain.Ports;

namespace EnvBooster.Application.Environments;

public class GetEnvironmentsUseCase(IEnvironmentRepository environmentRepository)
{
    private readonly IEnvironmentRepository _environmentRepository = environmentRepository;

    public IReadOnlyCollection<Environment> Execute()
    {
        return _environmentRepository.GetAll();
    }
}