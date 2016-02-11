using System;
using NHibernate.Validator.Constraints;

namespace Dixiton.DataAccess.Entities
{
    public abstract class EntityBase
    {
        /// <summary>
        /// 	Identifier of an entity
        /// </summary>
        public virtual String Id { get; set; }
    }
}