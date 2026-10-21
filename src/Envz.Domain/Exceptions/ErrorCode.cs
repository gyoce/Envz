namespace Envz.Domain.Exceptions;

public enum ErrorCode
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