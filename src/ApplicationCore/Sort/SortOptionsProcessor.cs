using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Helpers;
using RecipeManager.ApplicationCore.Interfaces;
using RecipeManager.ApplicationCore.Specifications;

namespace RecipeManager.ApplicationCore.Sort
{
    public class SortOptionsProcessor<T> where T : ISingleEntity
    {
        private readonly IReadOnlyCollection<string>? orderBy;

        public SortOptionsProcessor(IReadOnlyCollection<string>? orderBy)
        {
            this.orderBy = orderBy;
        }

        public IEnumerable<SortTerm> GetAllTerms()
        {
            if (orderBy == null)
            {
                yield break;
            }

            foreach (var term in orderBy)
            {
                if (string.IsNullOrWhiteSpace(term))
                {
                    continue;
                }

                var tokens = term.Split(' ');

                if (tokens.Length == 0)
                {
                    yield return new SortTerm { Name = term };
                    continue;
                }

                var descending = tokens.Length > 1 && tokens[1].Equals("desc", StringComparison.OrdinalIgnoreCase);

                yield return new SortTerm
                {
                    Name = tokens[0],
                    Descending = descending,
                };
            }
        }

        public IEnumerable<SortTerm> GetValidTerms()
        {
            var queryTerms = GetAllTerms().ToArray();
            if (!queryTerms.Any())
            {
                yield break;
            }

            var declaredTerms = GetTermsFromModel().ToArray();

            foreach (var term in queryTerms)
            {
                var declaredTerm = declaredTerms.SingleOrDefault(x => x.Name.Equals(term.Name, StringComparison.OrdinalIgnoreCase));

                if (declaredTerm == null)
                {
                    continue;
                }

                yield return new SortTerm
                {
                    Name = declaredTerm.Name,
                    EntityName = declaredTerm.EntityName,
                    Descending = term.Descending,
                    Default = declaredTerm.Default,
                };
            }
        }

        public IQueryable<T> Apply(IQueryable<T> query)
        {
            var terms = GetValidTerms().ToArray();

            if (!terms.Any())
            {
                terms = GetTermsFromModel().Where(t => t.Default).ToArray();
            }

            if (!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;
            var useThenBy = false;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<T>(term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<T>();

                // Build the LINQ expression backwards:
                // query = query.OrderBy(x => x.Property)

                // x => x.Property
                var key = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper.GetLambda(typeof(T), propertyInfo.PropertyType, obj, key);

                // query.OrderBy/ThenBy[Descending](x => x.Property)
                modifiedQuery = ExpressionHelper.CallOrderByOrThenBy(modifiedQuery, useThenBy, term.Descending, propertyInfo.PropertyType, keySelector);

                useThenBy = true;
            }

            return modifiedQuery;
        }

        public void Apply(Specification<T> spec)
        {
            if (spec == null)
            {
                throw new ArgumentNullException(nameof(spec));
            }

            spec.OrderByClauses.Clear();

            var terms = GetValidTerms().ToArray();

            if (!terms.Any())
            {
                terms = GetTermsFromModel().Where(t => t.Default).ToArray();
            }

            if (!terms.Any())
            {
                return;
            }

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<T>(term.EntityName ?? term.Name);
                var obj = ExpressionHelper.Parameter<T>();

                // x => x.Property
                var key = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);
                var keySelector = ExpressionHelper.GetLambda(typeof(T), typeof(object), obj, key) as Expression<Func<T, object>>;

                if (keySelector == null)
                {
                    continue;
                }

                spec.OrderByClauses.Add(new OrderByClause<T>(keySelector, term.Descending));
            }
        }

        private static IEnumerable<SortTerm> GetTermsFromModel()
        {
            return typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SortableAttribute>().Any())
                .Select(p => new SortTerm
                {
                    Name = p.Name, 
                    EntityName = p.GetCustomAttribute<SortableAttribute>().EntityProperty,
                    Default = p.GetCustomAttribute<SortableAttribute>().Default
                });
        }
    }
}
