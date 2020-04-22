using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> query, bool isSingleResultQuery) where T : SingleEntity
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

        public static async Task<PagedResults<T>> ApplyAsync<T>(this IQueryable<T> inputQuery, ISpecification<T>? specification) where T : SingleEntity
        {
            specification ??= new Specification<T>();

            var query = specification.ServerCriteria.Aggregate(
                inputQuery, 
                (current, criterion) => current.Where(criterion)) ?? inputQuery;

            IOrderedQueryable<T>? ordered = null;
            foreach (var clause in specification.OrderByClauses)
            {
                if (ordered == null)
                {
                    ordered = clause.Descending ? query.OrderByDescending(clause.Expression) : query.OrderBy(clause.Expression);
                }
                else
                {
                    ordered = clause.Descending ? ordered.ThenByDescending(clause.Expression) : ordered.ThenBy(clause.Expression);
                }

                query = ordered;
            }

            // TODO: Cache compile calls. If no expression analysis is done, ClientCauses could store them directly instead of the expressions
            var raw = await query.ToListAsync().ConfigureAwait(false);
            var filtered = specification.ClientCriteria.Aggregate(
                (IEnumerable<T>)raw,
                (current, criterion) => current.Where(criterion.Compile()))
                .ToArray();

            var result = new PagedResults<T>
            {
                TotalSize = filtered.Length
            };

            if (specification.IsPagingEnabled)
            {

                filtered = filtered
                    .Skip(specification.Skip)
                    .Take(specification.Take)
                    .ToArray();
            }

            result.Items = filtered;

            return result;
        }
    }
}
