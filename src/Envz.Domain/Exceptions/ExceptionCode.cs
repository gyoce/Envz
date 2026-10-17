namespace Envz.Domain.Exceptions;

public enum ExceptionCode
{
    ToolInvalidName,
    ToolInvalidPath,
    WorkspaceInvalidName,

    ConfigurationInvalid,

    CreateWorkspaceToolNotFound,
    CreateToolAlreadyExists,
    CreateWorkspaceAlreadyExists,
    DeleteToolStillUsed,
    DeleteToolNotFound,
}