using Envz.Domain.Entities;
using Envz.Domain.Exceptions;
using Envz.Domain.Exceptions.Tools;
using Envz.Domain.Extensions;
using Envz.Domain.Ports;
using Envz.Domain.Validation;

namespace Envz.Application.Validators;

public class ToolValidator(IToolRepository toolRepository)
{
    public ValidationResult<Tool> ValidateCreation(string name, string path, byte[]? icon = null)
    {
        ValidationResult<Tool> result = Build(name, path, icon);
        if (!result.IsValid)
            return result;

        if (toolRepository.Exists(result.Value.Name))
            return ValidationResult<Tool>.Fail(AlreadyExists(result.Value.Name));

        return result;
    }

    public ValidationResult<Tool> ValidateEdition(Tool original, string name, string path, byte[]? icon)
    {
        ValidationResult<Tool> result = Build(name, path, icon);
        if (!result.IsValid)
            return result;

        bool isRenamed = !original.Name.EqualsIgnoringCase(result.Value.Name);
        if (isRenamed && toolRepository.Exists(result.Value.Name))
            return ValidationResult<Tool>.Fail(AlreadyExists(result.Value.Name));

        return result;
    }

    private static ValidationResult<Tool> Build(string name, string path, byte[]? icon = null)
    {
        try
        {
            Tool tool = new(name, path, icon);
            return ValidationResult<Tool>.Success(tool);
        }
        catch (ValidationException ex)
        {
            return ValidationResult<Tool>.Fail(ex);
        }
    }

    private static ToolAlreadyExistsException AlreadyExists(string toolName)
    {
        return new ToolAlreadyExistsException(
            $"Could not create tool `{toolName}` because it already exists.",
            ErrorCode.CreateToolAlreadyExists,
            toolName
        );
    }
}