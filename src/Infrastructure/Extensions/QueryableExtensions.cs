﻿namespace RecipeManager.Infrastructure.Extensions
{
    using System.Linq;
    using System.Reflection;

    using Microsoft.EntityFrameworkCore;

    using RecipeManager.ApplicationCore.Attributes;
    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Extensions;

    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query, bool isSingleResultQuery) where T : BaseEntity
        {
            foreach (var prop in typeof(T).GetTypeInfo().GetAllProperties())
            {
                var attribute = prop.GetCustomAttribute<IncludeAttribute>();
                if (attribute == null)
                {
                    continue;
                }

                if (attribute is IncludeInAllQueriesAttribute || (attribute is IncludeInSingleQueriesAttribute && isSingleResultQuery))
                {
                    query = query.Include(prop.Name);
                }
            }

            return query;
        }
    }
}