namespace EnvBooster.Domain.Ports;

public interface IEnvironmentRepository
{
    IReadOnlyCollection<Environment> GetAll();
    void Save(Environment environment);
}