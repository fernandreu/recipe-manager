using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.Infrastructure.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> IncludeAll<T>(this IQueryable<T> self, bool isSingleResultQuery)
            where T : class, ISingleEntity
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
                    self = self.Include(prop.Name);
                }
            }

            return self;
        }

        public static async Task<PagedResults<TSource>> ApplyAsync<TSource, TDest>(this IQueryable<TSource> self, Specification<TDest>? specification, IMapper mapper)
            where TSource : ISingleEntity
            where TDest : ISingleEntity
        {
            specification ??= new Specification<TDest>();
            var destSpec = mapper.Map<Specification<TSource>>(specification);
            
            var query = destSpec.ServerCriteria.Aggregate(
                self, 
                (current, criterion) => current.Where(criterion)) ?? self;

            IOrderedQueryable<TSource>? ordered = null;
            foreach (var clause in destSpec.OrderByClauses ?? Enumerable.Empty<OrderByClause<TSource>>())
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
            var filtered = destSpec.ClientCriteria.Aggregate(
                (IEnumerable<TSource>)raw,
                (current, criterion) => current.Where(criterion.Compile()))
                .ToArray();

            var result = new PagedResults<TSource>
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
