using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.Infrastructure.Data
{
    public class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> specification)
            where T : BaseEntity
        {
            // modify the IQueryable using the specification's criteria expression
            var query = specification.Criteria.Aggregate(inputQuery, 
                (current, criterion) => current.Where(criterion));

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                (current, include) => current.Include(include));

            // Include any string-based include statements
            query = specification.IncludeStrings.Aggregate(query,
                (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            IOrderedQueryable<T> ordered = null;
            foreach (var (expr, desc) in specification.OrderBy)
            {
                if (ordered == null)
                {
                    ordered = desc ? query.OrderByDescending(expr) : query.OrderBy(expr);
                }
                else
                {
                    ordered = desc ? ordered.ThenByDescending(expr) : ordered.ThenBy(expr);
                }

                query = ordered;
            }

            // Apply paging if enabled
            if (specification.IsPagingEnabled)
            {
                query = query
                    .Skip(specification.Skip)
                    .Take(specification.Take);
            }

            return query;
        }
    }
}
