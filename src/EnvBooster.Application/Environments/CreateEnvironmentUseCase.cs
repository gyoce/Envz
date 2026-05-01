using EnvBooster.Domain.Ports;

namespace EnvBooster.Application.Environments;

public class CreateEnvironmentUseCase(IEnvironmentRepository environmentRepository)
{
    public void Execute(string name)
    {
        environmentRepository.Save(name);
    }
}