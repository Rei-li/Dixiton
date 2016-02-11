using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Dixiton.Dtos;
using Dixiton.Logic;
using Dixiton.Logic.Commands;
using Dixiton.Logic.Queries;
using Dixiton.Models;

namespace Dixiton.Controllers.Api
{
    public class UserController : ApiController
    {

        #region Properties
        /// <summary>
        /// Interface for commands dispatchering
        /// </summary>
        public ICommandDispatcher CommandQueryDispatcher { get; set; }
        #endregion Properties


        //// GET: api/UserApi
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/User/5
        public IHttpActionResult GetUser(string id)
        {
            var query = new GetUserQuery { UserId = id };
            var result = CommandQueryDispatcher.ExecuteQuery(query);
            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok((UserDto)result.Data);
        }

        // POST: api/User/Login
        public IHttpActionResult Login([FromBody]LoginModel model)
        {
            var query = new IdentifyUserQuery { Login = model.Login, Password = model.Password};
            var result = CommandQueryDispatcher.ExecuteQuery(query);
            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(((UserDto)result.Data).Id);

        }

        // PUT: api/User/Register
        [Route("~/api/user/Register")]
        public IHttpActionResult Register(RegisterModel model)
        {
           // var userDto = Mapper.Map<RegisterModel, UserDto>(model);

             var userDto = new UserDto()
             {
                 Id = model.Id,
                 Login =  model.Login,
                 Password = model.Password,
                 ConfirmPassword = model.ConfirmPassword,
                 Email = model.Email
             };
            var command = new SaveUserCommand() { UserDto = userDto };
            var result = CommandQueryDispatcher.ExecuteCommand(command);
            if (result.Data == null)
            {
                return NotFound();
            }

            return Ok(((UserDto)result.Data).Id);

        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
