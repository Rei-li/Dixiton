using System;
using System.Data;
using NHibernate;

namespace Dixiton.DataAccess.NHibernate
{
    public interface ISessionManager
    {
        /// <summary>
        /// 	Opens current SQL session.
        /// </summary>
        void OpenSession();

        /// <summary>
        /// 	Close current SQL session.
        /// </summary>
        void CloseSession();

        /// <summary>
        /// 	Begins SQL transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// 	Begins SQL transaction.
        /// </summary>
        void BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// 	Commits SQL transaction.
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 	Rollbacks SQL transaction and close session.
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// Current session
        /// </summary>
        ISession CurrentSession { get; }
    }
}