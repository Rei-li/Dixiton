using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dixiton.DataAccess.Daos;
using Dixiton.Dtos;
using Dixiton.Logic.Commands;
using Dixiton.Logic.Queries;
using Dixiton.Logic.Validation;

namespace Dixiton.Logic.Blos
{
    public class UserBlo : BloBase
    {
        #region [ Properties ]

        public UserDao UserDao { get; set; }

        #endregion

        public UserBlo(ValidationManager validationManager) : base(validationManager)
        {
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        private ExecutionResult GetUser(QueryBase query)
        {
            return new ExecutionResult { Data = UserDao.Get(((GetUserQuery)query).UserId) };
        }


        private ExecutionResult IdentifyUser(QueryBase query)
        {
            var identifyUserQuery = ((IdentifyUserQuery)query);
            var login = identifyUserQuery.Login;
            var password = identifyUserQuery.Password;
            //Validate(user);
            return new ExecutionResult { Data = UserDao.Get(login, password) };
        }



        private ExecutionResult Save(CommandBase command)
        {
            var user = ((SaveUserCommand)command).UserDto;
            //Validate(user);
            user.Id = Guid.NewGuid().ToString();
            var id = UserDao.Save(user);
            return new ExecutionResult { Data = UserDao.Get(id) };
        }



        public override void RegisterCommands()
        {
            CommandQueryRegistrator.RegisterCommand(SaveUserCommand.CMD_ID_SAVE_USER, Save);
            CommandQueryRegistrator.RegisterQuery(GetUserQuery.CMD_ID_GET_USER, GetUser);
            CommandQueryRegistrator.RegisterQuery(IdentifyUserQuery.CMD_ID_IDENTIFY_USER, IdentifyUser);
        }
    }
}
