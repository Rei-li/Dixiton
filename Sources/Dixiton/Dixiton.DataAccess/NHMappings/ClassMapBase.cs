﻿using System;
using System.Linq.Expressions;
using FluentNHibernate.Mapping;
using FluentNHibernate.Utils.Reflection;
using Dixiton.DataAccess.Entities;

namespace Dixiton.DataAccess.NHMappings
{
    /// <summary>
    /// 	Represents base mapping class for entity.
    /// </summary>
    /// <typeparam name = "T"></typeparam>
    public class ClassMapBase<T> : ClassMap<T>
        where T : EntityBase
    {

        public ClassMapBase()
        {
            Table(typeof(T).Name.Replace("Entity", String.Empty));
            Id(v => v.Id);
        }

        protected string Column(Expression<Func<T, object>> expression)
        {
            return ReflectionHelper.GetMember(expression).Name;
        }

        protected string Column<TC>(Expression<Func<TC, object>> expression)
        {
            return ReflectionHelper.GetMember(expression).Name;
        }
    }
}