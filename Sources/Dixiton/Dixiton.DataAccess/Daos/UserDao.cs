using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dixiton.DataAccess.Entities;
using Dixiton.Dtos;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Dixiton.DataAccess.Daos
{
    public class UserDao : DaoBase
    {
        /// <summary>
        /// Get User
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns></returns>
        public UserDto Get(string userId)
        {
            UserEntity userAlias = null;
          

            var restrictions = Restrictions.Eq(Projections.Property(() => userAlias.Id), userId);
            

            var projections = new[]
            {
                Projections.Alias(Projections.Property(()=>userAlias.Id), UserDto.PROP_ID),
                Projections.Alias(Projections.Property(()=>userAlias.Login), UserDto.PROP_LOGIN),
                Projections.Alias(Projections.Property(()=>userAlias.Password), UserDto.PROP_PASSWORD),
                Projections.Alias(Projections.Property(()=>userAlias.Email), UserDto.PROP_EMAIL),
              
            };

            var dto =
            SessionManager.CurrentSession.QueryOver(() => userAlias)
               
                .Select(projections)
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<UserDto>())
                .SingleOrDefault<UserDto>();

            return dto;
        }


   
        public UserDto Get(string login, string password)
        {
            UserEntity userAlias = null;

            var restrictions = Restrictions.And(
                Restrictions.Eq(Projections.Property(() => userAlias.Login), login),
                Restrictions.Eq(Projections.Property(() => userAlias.Password), password)
                ) ;

            var projections = new[]
            {
                Projections.Alias(Projections.Property(()=>userAlias.Id), UserDto.PROP_ID),
                Projections.Alias(Projections.Property(()=>userAlias.Login), UserDto.PROP_LOGIN),
                Projections.Alias(Projections.Property(()=>userAlias.Password), UserDto.PROP_PASSWORD),
                Projections.Alias(Projections.Property(()=>userAlias.Email), UserDto.PROP_EMAIL),
              
            };

            var dto =
            SessionManager.CurrentSession.QueryOver(() => userAlias)
                .Select(projections)
                .And(restrictions)
                .TransformUsing(Transformers.AliasToBean<UserDto>())
                .SingleOrDefault<UserDto>();

            return dto;
        }



        /// <summary>
        /// Get User id by name
        /// </summary>
        /// <returns>Id</returns>
        public int GetId(string login, string password)
        {
            UserEntity userAlias = null;
            return SessionManager.CurrentSession.QueryOver(() => userAlias)
                .Where(
                Restrictions.And(
                Restrictions.Eq(Projections.Property(() => userAlias.Login), login),
            Restrictions.Eq(Projections.Property(() => userAlias.Password), password)
                )
                )
                .Select(Projections.Property(() => userAlias.Id)).List<int>().FirstOrDefault();
        }

        ///// <summary>
        ///// Get User name by id
        ///// </summary>
        ///// <returns>Id</returns>
        //public string GetName(int id)
        //{
        //    UserEntity UserAlias = null;
        //    return SessionManager.CurrentSession.QueryOver(() => UserAlias)
        //        .Where(Restrictions.Eq(Projections.Property(() => UserAlias.Id), id))
        //        .Select(Projections.Property(() => UserAlias.UserName)).List<string>().FirstOrDefault();
        //}

        ///// <summary>
        ///// Get User acronim by id
        ///// </summary>
        ///// <returns>Id</returns>
        //public string GetAcronim(int id)
        //{
        //    UserEntity alias = null;
        //    return SessionManager.CurrentSession.QueryOver(() => alias)
        //        .Where(Restrictions.Eq(Projections.Property(() => alias.Id), id))
        //        .Select(Projections.Property(() => alias.Acronym)).List<string>().FirstOrDefault();
        //}


        /// <summary>
        /// Is User with current acronym
        /// </summary>
        /// <param name="login">acronym</param>
        /// <param name="password">Id</param>
        /// <returns></returns>
        public bool IsExist(string login, string password)
        {
            return IsExist<UserEntity>(user => user.Login == login && user.Password != password);
        }

        /// <summary>
        ///Create User
        /// </summary>
        /// <param name="user">User</param>
        /// <returns></returns>
        public string Save(UserDto user)
        {
            return Save<UserDto, UserEntity>(user);
        }

        ///// <summary>
        ///// Get list of Users
        ///// </summary>
        ///// <param name="UserSearchString">The User search string.</param>
        ///// <param name="includeArchived">Include archived Users</param>
        ///// <returns>
        ///// list of User lookups
        ///// </returns>
        //public List<LookupDto> GetLookup(string UserSearchString, bool includeArchived = false)
        //{
        //    //Get list of occurences that starts with UserSearchString
        //    var queryOver = GetLookupQueryOver<UserEntity>(UserDto.PROP_ACRONYM);
        //    if (!string.IsNullOrEmpty(UserSearchString))
        //    {
        //        queryOver.Where(
        //            Restrictions.On<UserEntity>(e => e.Acronym)
        //                .IsInsensitiveLike(string.Format(STARTS_WITH_FORMAT, UserSearchString)) ||
        //            Restrictions.On<UserEntity>(e => e.UserName)
        //                .IsInsensitiveLike(string.Format(STARTS_WITH_FORMAT, UserSearchString)));
        //    }

        //    if (!includeArchived)
        //        queryOver.Where(value => !value.IsArchived);

        //    var resultStartsWith = LookupQueryResultTransform(queryOver);

        //    //Get list of occurences that contains UserSearchString
        //    queryOver = GetLookupQueryOver<UserEntity>(UserDto.PROP_ACRONYM);
        //    if (!string.IsNullOrEmpty(UserSearchString))
        //    {
        //        queryOver.Where(
        //            Restrictions.On<UserEntity>(e => e.Acronym)
        //                .IsInsensitiveLike(string.Format(CONTAINS_FORMAT, UserSearchString)) ||
        //            Restrictions.On<UserEntity>(e => e.UserName)
        //                .IsInsensitiveLike(string.Format(CONTAINS_FORMAT, UserSearchString)));
        //    }

        //    if (!includeArchived)
        //        queryOver.Where(value => !value.IsArchived);

        //    var resultContains = LookupQueryResultTransform(queryOver);

        //    //Merge results
        //    var merged = resultStartsWith.ToList();
        //    merged.AddRange(resultContains.Where(x => resultStartsWith.All(y => y.Id != x.Id)));

        //    return merged;
        //}

        ///// <summary>
        ///// Gets the Users lookup.
        ///// </summary>
        ///// <param name="UserIds">The User ids.</param>
        ///// <returns></returns>
        //public List<LookupDto> GetUserNameLookups(List<int> UserIds)
        //{

        //    var queryOver = GetLookupQueryOver<UserEntity>(UserDto.PROP_ACRONYM);
        //    if (UserIds != null && UserIds.Count > 0)
        //    {
        //        queryOver.Where(Restrictions.In(Projections.Property(UserDto.PROP_ID), UserIds));
        //    }

        //    return LookupQueryResultTransform(queryOver);
        //}

        ///// <summary>
        ///// Get list of Users
        ///// </summary>
        ///// <returns>list of Users</returns>
        //public IList<UserListDto> GetUsers(bool includeArchived = false)
        //{
        //    UserEntity UserAlias = null;
        //    CustomerEntity customerAlias = null;
        //    DepartmentEntity departmentAlias = null;

        //    var restrictions = includeArchived
        //        ? (ICriterion)Restrictions.Conjunction()
        //        : Restrictions.Eq(Projections.Property(() => UserAlias.IsArchived), false);

        //    var Projections = new[]
        //    {
        //        Projections.Alias(Projections.Property(()=>UserAlias.Id), UserListDto.PROP_ID),
        //        Projections.Alias(Projections.Property(()=>UserAlias.Version), UserListDto.PROP_VERSION),
        //        Projections.Alias(Projections.Property(()=>UserAlias.UserName), UserListDto.PROP_NAME),
        //        Projections.Alias(Projections.Property(()=>UserAlias.UserType), UserListDto.PROP_UserTYPE),
        //        Projections.Alias(Projections.Property(()=>UserAlias.Acronym), UserListDto.PROP_ACRONYM),
        //        Projections.Alias(Projections.Property(()=>UserAlias.IsArchived), UserListDto.PROP_ISARCHIVED),
        //        Projections.Alias(Projections.Property(()=>customerAlias.Name), UserListDto.PROP_CUSTOMER),
        //        Projections.Alias(Projections.Property(()=>departmentAlias.Acronym), UserListDto.PROP_DEPARTMENT),
        //    };

        //    IList<UserListDto> dtos =
        //    SessionManager.CurrentSession.QueryOver(() => UserAlias)
        //        .JoinAlias(x => x.CustomerIdObject, () => customerAlias)
        //        .Left.JoinAlias(x => x.DepartmentIdObject, () => departmentAlias)
        //        .Select(Projections)
        //        .And(restrictions)
        //        .TransformUsing(Transformers.AliasToBean<UserListDto>())
        //        .List<UserListDto>();

        //    return dtos;
        //}

        ///  <summary>
        /// Delete User
        ///  </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="version">version</param>
        /// <returns></returns>
        public void Delete(int UserId, int version)
        {
            Delete<UserEntity>(UserId, version);
        }
    }
}
