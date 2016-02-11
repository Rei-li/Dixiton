using System;
using Dixiton.Common;

namespace Dixiton.Dtos
{
    public class EntityDtoBase
    {
        #region [ Property names ]

        public static string PROP_ID = TypeHelper<EntityDtoBase>.PropertyName(x => x.Id);
        //public static string PROP_VERSION = TypeHelper<EntityDtoBase>.PropertyName(x => x.Version);

        #endregion

        /// <summary>
        /// Gets or sets Id
        /// </summary>
        public string Id { get; set; }


        ///// <summary>
        ///// Gets or sets version
        ///// </summary>
        //public int Version { get; set; }
    }
}
