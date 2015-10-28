using Dixiton.Logic.Commands;
using Dixiton.Logic.Queries;

namespace Dixiton.Logic
{
    /// <summary>
    /// Interface for command dispatchering
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Eхeсute command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Command execution result</returns>
        ExecutionResult ExecuteCommand(CommandBase command);

        /// <summary>
        /// Eхeсute query
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <returns>Query execution result</returns>
        ExecutionResult ExecuteQuery(QueryBase query);
    }
}