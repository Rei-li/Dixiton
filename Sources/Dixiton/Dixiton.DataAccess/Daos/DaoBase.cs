using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Dixiton.DataAccess.Entities;
using Dixiton.DataAccess.NHibernate;
using Dixiton.Dtos;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace Dixiton.DataAccess.Daos
{
    /// <summary>
    /// Base DAO class 
    /// </summary>
    public abstract class DaoBase
    {
        protected const string CONTAINS_FORMAT = "%{0}%";
        protected const string STARTS_WITH_FORMAT = "{0}%";

        /// <summary>
        /// Session manager 
        /// </summary>
        public ISessionManager SessionManager { get; set; }

        /// <summary>
        ///Save entity
        /// </summary>
        /// <returns></returns>
        protected int Save<TDto, TEntity>(TDto dto)
            where TDto : EntityDtoBase
            where TEntity : EntityBase
        {
            TEntity entity = Mapper.Map<TDto, TEntity>(dto);
            SessionManager.CurrentSession.SaveOrUpdate(entity);
            SessionManager.CurrentSession.Flush();
            return entity.Id;
        }

        /// <summary>
        ///Save entities
        /// </summary>
        /// <returns></returns>
        protected void Save<TDto, TEntity>(IList<TDto> dtos)
            where TDto : EntityDtoBase
            where TEntity : EntityBase
        {
            foreach (var dto in dtos)
            {
                TEntity entity = Mapper.Map<TDto, TEntity>(dto);
                SessionManager.CurrentSession.SaveOrUpdate(entity);
            }
            SessionManager.CurrentSession.Flush();
        }

        /// <summary>
        /// Save entity
        /// </summary>
        protected int Save<TDto, TEntity>(TDto dto, int id)
            where TDto : EntityDtoBase
            where TEntity : EntityBase
        {
            TEntity entity = Mapper.Map<TDto, TEntity>(dto);
            SessionManager.CurrentSession.Save(entity, id);
            SessionManager.CurrentSession.Flush();
            return entity.Id;
        }

        /// <summary>
        ///Delete entity
        /// </summary>
        /// <returns></returns>
        /// TODO: CR: SAS-FIX: It shouldn't be a public method
        protected void Delete<TEntity>(int id, int version)
            where TEntity : EntityBase
        {
            var entity = SessionManager.CurrentSession.Get<TEntity>(id);
            if (entity != null )
            {
                SessionManager.CurrentSession.Delete(entity);
                SessionManager.CurrentSession.Flush();
            }
            else
            {
                throw new ConcurrencyException();
            }
        }
        
        /// <summary>
        ///Check is exist entity in bd
        /// </summary>
        /// <returns></returns>
        /// /// TODO: CR: SAS-FIX: It shouldn't be a public method
        protected bool IsExist<T>(Expression<Func<T, bool>> func)
            where T : EntityBase
        {
            return SessionManager.CurrentSession.QueryOver<T>().Select(
                new[] { Projections.Constant(true) }).Where(func).Take(1).SingleOrDefault<bool>();
        }



        ///// <summary>
        ///// Get Lookup Query
        ///// </summary>
        ///// <typeparam name="TEntity"> type of entities selected in query</typeparam>
        ///// <param name="valuePropertyName">name of property for value in LookupDto </param>
        ///// <returns></returns>
        ///// /// TODO: CR: NSO-FIX: It shouldn't be a public method
        //protected IQueryOver<TEntity, TEntity> GetLookupQueryOver<TEntity>(string valuePropertyName)
        //    where TEntity : EntityBase
        //{
        //    TEntity alias = null;
        //    var queryOver = SessionManager.CurrentSession.QueryOver(() => alias)
        //        .Select(Projections.Property(() => alias.Id).As(LookupDto.PROP_ID),
        //            Projections.Property(valuePropertyName).As(LookupDto.PROP_VALUE));
        //    return queryOver;
        //}

        ///// <summary>
        ///// transform qury resalt to list of LookupDto
        ///// </summary>
        ///// <typeparam name="TEntity">type of entities selected in query</typeparam>
        ///// <param name="queryOver">Lookup query</param>
        ///// <param name="onlyUniqueValues"></param>
        ///// <returns></returns>
        ///// /// TODO: CR: NSO-FIX: It shouldn't be a public method
        ///// TODO: CR: NSO-FIX: Resalt->Result
        //protected List<LookupDto> LookupQueryResultTransform<TEntity>(IQueryOver<TEntity, TEntity> queryOver,
        //    bool onlyUniqueValues = false)
        //{
        //    IEnumerable<LookupDto> query =
        //        queryOver
        //            .TransformUsing(Transformers.AliasToBean<LookupDto>())
        //            .List<LookupDto>();

        //    if (onlyUniqueValues)
        //    {
        //        query = query.GroupBy(value => value.Value).Select(g => g.First());
        //    }
        //    return query.OrderBy(value => value.Value).ToList();
        //}

        protected TEntity Get<TEntity>(int id)
              where TEntity : EntityBase
        {
            return SessionManager.CurrentSession.Get<TEntity>(id);
        }

        public TDto Get<TEntity, TDto>(int id)
            where TEntity : EntityBase
            where TDto : EntityDtoBase
        {
            var entity = SessionManager.CurrentSession.Get<TEntity>(id);
            var dto = Mapper.Map<TEntity, TDto>(entity);
            return dto;
        }
    }
}