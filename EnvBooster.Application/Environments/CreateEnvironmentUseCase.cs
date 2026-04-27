using EnvBooster.Domain.Ports;

namespace EnvBooster.Application.Environments;

public class CreateEnvironmentUseCase(IEnvironmentRepository environmentRepository)
{
    private readonly IEnvironmentRepository _environmentRepository = environmentRepository;

    public void Execute(string name)
    {
        _environmentRepository.Save(name);
    }
}