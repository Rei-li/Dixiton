using System;

namespace Dixiton.Logic.Queries
{
    /// <summary>
    /// Command could only read data from DB 
    /// </summary>
    public abstract class QueryBase
    {
        /// <summary>
        /// Query identifier
        /// </summary>
        public abstract Guid QueryId { get; }
    }
}