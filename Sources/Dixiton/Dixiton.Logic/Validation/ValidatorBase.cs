using System;
using System.Collections.Generic;
using Dixiton.Common.Validation;
using Dixiton.Dtos;

namespace Dixiton.Logic.Validation
{
    public abstract class ValidatorBase
    {
        /// <summary>
        /// Validates the specified activity dto.
        /// </summary>
        /// <param name="activityDto">The activity dto.</param>
        /// <returns>List of errors</returns>
        public abstract List<ErrorInfo> Validate(EntityDtoBase activityDto);

        public abstract IEnumerable<Type> GetSupportedTypes();
    }
}
