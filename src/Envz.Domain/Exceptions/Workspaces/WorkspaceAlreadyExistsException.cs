namespace Envz.Domain.Exceptions.Workspaces;

public class WorkspaceAlreadyExistsException(string message, ErrorCode code, string workspaceName)
    : EnvzException(message, code, workspaceName);