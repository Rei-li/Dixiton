using Dixiton.Common.Validation;
using Dixiton.Dtos;
using Dixiton.Logic.Validation;

namespace Dixiton.Logic.Blos
{
    /// <summary>
    /// Base business logic
    /// </summary>
    public abstract class BloBase
    {
        /// <summary>
        /// Register commands handlers
        /// </summary>
        public abstract void RegisterCommands();

        /// <summary>
        /// Command registrator interface
        /// </summary>
        public ICommandRegistrator CommandQueryRegistrator { get; set; }
        
        protected ValidationManager ValidationManager { get; set; }

        protected BloBase(ValidationManager validationManager)
        {
            ValidationManager = validationManager;
        }

        /// <summary>
        /// Validate entity dto
        /// </summary>
        /// <param name="entityDto">entity Dto</param>
        /// <returns></returns>
        protected void Validate(EntityDtoBase entityDto)
        {
            var errors = ValidationManager.Validate(entityDto);
            
            if (errors.Count != 0)
                throw new ValidationException(errors);
        }
    }
}