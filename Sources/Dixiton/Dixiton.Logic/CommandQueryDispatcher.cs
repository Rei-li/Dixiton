using System;
using System.Collections.Generic;
using System.Data;
using Common.Logging;
using Dixiton.Common.Validation;
using Dixiton.DataAccess.NHibernate;
using Dixiton.Logic.Commands;
using Dixiton.Logic.Queries;

namespace Dixiton.Logic
{
    /// <summary>
    /// Dispatchering command execution
    /// </summary>
    public class CommandQueryDispatcher : ICommandDispatcher, ICommandRegistrator
    {

        #region [ Constants ]

        private const string COMMAND_FAILED_MESSAGE = "Command failed: CommandId[{0}], CommandData[{1}]";
        private const string QUERY_FAILED_MESSAGE = "Query failed: QueryId[{0}], QueryData[{1}]";
        private const string NULL_COMMAND_MESSAGE = "Can't execute command = null";
        private const string NULL_QUERY_MESSAGE = "Can't execute query = null";
        private const string QUERY_REGISTRATION_ERROR_MESSAGE = "Query with id={0} already registered";
        private const string COMMAND_REGISTRATION_ERROR_MESSAGE = "Command with id={0} already registered";


        #endregion

        #region [ Fields ]

        private ILog _log = LogManager.GetLogger(typeof(CommandQueryDispatcher));

        private Dictionary<Guid, Func<CommandBase, ExecutionResult>> _commandsHandlers =
            new Dictionary<Guid, Func<CommandBase, ExecutionResult>>();

        private Dictionary<Guid, Func<QueryBase, ExecutionResult>> _queriesHandlers =
            new Dictionary<Guid, Func<QueryBase, ExecutionResult>>();

        private readonly ISessionManager _sessionManager;
        #endregion


        public CommandQueryDispatcher(ISessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        /// <summary>
        /// Regiter command handler
        /// </summary>
        /// <param name="commandId">Command identifier</param>
        /// <param name="commandHandler">Command handler</param>
        public void RegisterCommand(Guid commandId, Func<CommandBase, ExecutionResult> commandHandler)
        {
            if (_commandsHandlers.ContainsKey(commandId))
            {
                throw new ApplicationException(COMMAND_REGISTRATION_ERROR_MESSAGE);
            }

            _commandsHandlers.Add(commandId, commandHandler);
        }

        /// <summary>
        /// Regiter query handler
        /// </summary>
        /// <param name="queryId">Query identifier</param>
        /// <param name="queryHandler">Query handler</param>
        public void RegisterQuery(Guid queryId, Func<QueryBase, ExecutionResult> queryHandler)
        {
            if (_queriesHandlers.ContainsKey(queryId))
            {
                throw new ApplicationException(QUERY_REGISTRATION_ERROR_MESSAGE);
            }

            _queriesHandlers.Add(queryId, queryHandler);
        }

        /// <summary>
        /// ExecuteCommand command 
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Execution result</returns>
        public ExecutionResult ExecuteCommand(CommandBase command)
        {
            ExecutionResult result;

            if (command == null)
            {
                throw new ApplicationException(NULL_COMMAND_MESSAGE);
            }

            try
            {
                Func<CommandBase, ExecutionResult> commandHandler = _commandsHandlers[command.CommandId];
                result = TryExecute(commandHandler, command, IsolationLevel.ReadCommitted);
            }
            catch (ValidationException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                //_log.ErrorFormat(COMMAND_FAILED_MESSAGE, ex, command.CommandId, command);
                throw;
            }

            return result;
        }

        /// <summary>
        /// ExecuteCommand query 
        /// </summary>
        /// <param name="query">Query to execute</param>
        /// <returns>Execution result</returns>
        public ExecutionResult ExecuteQuery(QueryBase query)
        {
            ExecutionResult result;

            if (query == null)
            {
                throw new ApplicationException(NULL_QUERY_MESSAGE);
            }

            try
            {
                Func<QueryBase, ExecutionResult> queryHandler = _queriesHandlers[query.QueryId];
                result = TryExecute(queryHandler, query, IsolationLevel.ReadCommitted);
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //_log.ErrorFormat(QUERY_FAILED_MESSAGE, ex, query.QueryId, query);
                throw;
            }

            return result;
        }

        protected virtual TResult TryExecute<TParam, TResult>(Func<TParam, TResult> expression, TParam param, IsolationLevel isolationLevel)
        {
            try
            {
                _sessionManager.OpenSession();
                _sessionManager.BeginTransaction(isolationLevel);
                TResult rez = expression(param);
                _sessionManager.CommitTransaction();
                return rez;
            }
            catch
            {
                _sessionManager.RollbackTransaction();
                throw;
            }
            finally
            {
                _sessionManager.CloseSession();
            }
        }
    }
}