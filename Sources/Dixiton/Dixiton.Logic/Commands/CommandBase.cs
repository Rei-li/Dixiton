using System;

namespace Dixiton.Logic.Commands
{
    /// <summary>
    /// Base command 
    /// </summary>
    public abstract class CommandBase
    {
        /// <summary>
        /// Command identifier
        /// </summary>
        public abstract Guid CommandId { get; }
    }
}