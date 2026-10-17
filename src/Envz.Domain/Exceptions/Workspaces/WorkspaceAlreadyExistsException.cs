namespace Envz.Domain.Exceptions.Workspaces;

public class WorkspaceAlreadyExistsException(string message, ExceptionCode code, string workspaceName)
    : EnvzException(message, code, workspaceName);