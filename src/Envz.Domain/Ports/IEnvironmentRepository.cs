namespace Envz.Domain.Ports;

public interface IEnvironmentRepository
{
    IReadOnlyCollection<Environment> GetAll();
    void Save(Environment environment);
}