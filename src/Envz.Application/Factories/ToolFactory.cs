using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Ports;
using Envz.Domain.Validation;

namespace Envz.Application.Factories;

public class ToolFactory(IToolRepository toolRepository)
{
    public ValidationResult<Tool> Create(string name, string path, byte[]? icon = null)
    {
        Tool tool;

        try
        {
            tool = new Tool(name, path, icon);
        }
        catch (ValidationException ex)
        {
            return ValidationResult<Tool>.Fail(ex);
        }

        if (toolRepository.Exists(tool.Name))
        {
            return ValidationResult<Tool>.Fail(new ToolAlreadyExistsException(
                $"Could not create tool `{tool.Name}` because it already exists.",
                ErrorCode.CreateToolAlreadyExists,
                tool.Name)
            );
        }

        return ValidationResult<Tool>.Success(tool);
    }
}