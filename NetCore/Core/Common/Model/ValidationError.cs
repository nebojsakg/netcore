using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Common.Model
{
    public class ValidationError : Exception
    {
        public List<ApiError> Errors = new List<ApiError>();

        public ValidationError(IList<ValidationFailure> failures)
        {
            if (failures != null)
            {
                failures.ToList().ForEach(failure =>
                {
                    Errors.Add(new ApiError(400, failure.ErrorMessage));
                });
            }
        }
    }
}
