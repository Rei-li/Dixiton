using System;
using System.Collections.Generic;
using Dixiton.Common.Validation;
using Spring.Context;
using Dixiton.Dtos;

namespace Dixiton.Logic.Validation
{
    public class ValidationManager : IApplicationContextAware
    {
        public IApplicationContext ApplicationContext
        {
            set { this.ctx = value; }
        }

        private Dictionary<Type, ValidatorBase> _validators;
        private IApplicationContext ctx;


        public void Init()
        {
            IDictionary<string, ValidatorBase> validators = ctx.GetObjects<ValidatorBase>();
            _validators = new Dictionary<Type, ValidatorBase>(validators.Count);

            foreach (var validator in validators.Values)
            {
                foreach (Type dtoType in validator.GetSupportedTypes())
                {
                    _validators.Add(dtoType, validator);
                }
            }
        }


        public List<ErrorInfo> Validate(EntityDtoBase dto)
        {
            if (_validators == null)
            {
                Init();
            }
            var dtoType = dto.GetType();
            // ReSharper disable once PossibleNullReferenceException
            if (_validators.ContainsKey(dtoType))
            {
                return _validators[dtoType].Validate(dto);
            }

            throw new ArgumentException(string.Format("Could not find validator for type={0}", dtoType));
        }
    }
}
