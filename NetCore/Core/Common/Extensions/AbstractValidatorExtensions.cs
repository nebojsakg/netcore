using Core.Common.Commands;
using Core.Common.Model;
using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;

namespace Common.Extensions
{
    public static class AbstractValidatorExtensions
    {
        public static void ValidateCmd<T>(this AbstractValidator<T> validator, T command) where T : IBaseCommand
        {
            ValidationResult results = validator.Validate(command);

            if (!results.IsValid)
            {
                IList<ValidationFailure> failures = results.Errors;

                throw new ValidationError(failures);
            }
        }
    }
}
