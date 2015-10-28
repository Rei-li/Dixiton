using System;
using Dixiton.Logic.Commands;
using Dixiton.Logic.Queries;

namespace Dixiton.Logic
{
    /// <summary>
    /// Interface for command registration 
    /// </summary>
    public interface ICommandRegistrator
    {
        /// <summary>
        /// Register command
        /// </summary>
        /// <param name="commandId">Command identifier</param>
        /// <param name="commandHandler">Command execution handler</param>
        void RegisterCommand(Guid commandId, Func<CommandBase, ExecutionResult> commandHandler);

        /// <summary>
        /// Register query
        /// </summary>
        /// <param name="queryId">Query identifier</param>
        /// <param name="queryHandler">Query execution handler</param>
        void RegisterQuery(Guid queryId, Func<QueryBase, ExecutionResult> queryHandler);
    }
}