using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using Common.Logging;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Event;
using NHibernate.Linq;
using NHibernate.Validator.Cfg;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;

namespace Dixiton.DataAccess.NHibernate
{
    public class SessionManager : ISessionManager
    {
        #region [ Fields ]

        private static readonly ILog Log = LogManager.GetLogger(typeof(SessionManager).Name);
        private readonly ISessionFactory _factory;
        private readonly ISessionStorage _storage;
        private ValidatorEngine _validatorEngine;

        #endregion

        #region [ Properties ]

        /// <summary>
        /// 	Gets/Sets current NHibernate session.
        /// </summary>
        public ISession CurrentSession
        {
            get { return _storage.CurrentSession; }
            private set { _storage.CurrentSession = value; }
        }

        #endregion

        public SessionManager(ISessionStorage storage)
        {
            _storage = storage;

            Configuration configuration = new Configuration().Configure();

            InitValidator(configuration);

            configuration.EventListeners.FlushEntityEventListeners = new IFlushEntityEventListener[] { new FlushEntityEventListener() };

            var validateFieldsEventListener = new ValidateFieldsEventListener(_validatorEngine);
            configuration.EventListeners.PreInsertEventListeners = new IPreInsertEventListener[] { validateFieldsEventListener, new ValidatePreInsertEventListener() };
            configuration.EventListeners.PreUpdateEventListeners = new IPreUpdateEventListener[] { validateFieldsEventListener, new ValidatePreUpdateEventListener() };

            _factory = InitFluentMappings(configuration).BuildSessionFactory();
        }

        private static FluentConfiguration InitFluentMappings(Configuration configuration)
        {
            FluentConfiguration fluentConfiguration = Fluently.Configure(configuration);

            IEnumerable<Assembly> assemblies = GetDataAccessAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Assembly temp = assembly;
                fluentConfiguration.Mappings(v => v.FluentMappings.AddFromAssembly(temp));
            }

            return fluentConfiguration;
        }

        //определяем набор доменных сборок в которых определены мапинги для NH
        private static IEnumerable<Assembly> GetDataAccessAssemblies()
        {
            string currentDirectory = AssemblyDirectory();
            string[] strings = Directory.GetFiles(currentDirectory, "*DataAccess*.dll");

            List<Assembly> list = new List<Assembly>();

            strings.ForEach(x => list.Add(Assembly.LoadFile(x)));

            return list;
        }

        private void InitValidator(Configuration configuration)
        {
            _validatorEngine = new ValidatorEngine();

            _validatorEngine.Configure();

            ValidatorInitializer.Initialize(configuration, _validatorEngine);
        }


        private static string AssemblyDirectory()
        {
            var uri = new UriBuilder(Assembly.GetExecutingAssembly().CodeBase);
            return Path.GetDirectoryName(Uri.UnescapeDataString(uri.Path));
        }

        public void OpenSession()
        {
            CurrentSession = _factory.OpenSession();
            CurrentSession.FlushMode = FlushMode.Never;
        }

        public void CloseSession()
        {
            if (CurrentSession != null && CurrentSession.IsOpen)
            {
                CurrentSession.Close();
                CurrentSession = null;
            }
        }

        public void BeginTransaction()
        {
            if (CurrentSession != null)
            {
                CurrentSession.BeginTransaction();
            }
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            if (CurrentSession != null)
            {
                CurrentSession.BeginTransaction(isolationLevel);
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (CurrentSession != null && CurrentSession.Transaction.IsActive)
                {
                    CurrentSession.Transaction.Commit();
                }
            }
            catch (Exception e)
            {
                Log.Error("Ошибка при коммите транзакции", e);
                RollbackTransaction();
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (CurrentSession != null && CurrentSession.Transaction.IsActive)
                {
                    _storage.CurrentSession.Transaction.Rollback();
                }
            }
            catch (Exception e)
            {
                Log.Error("Ошибка при откате транзакции транзакции", e);
                throw;
            }
            finally
            {
                CloseSession();
            }
        }
    }
}