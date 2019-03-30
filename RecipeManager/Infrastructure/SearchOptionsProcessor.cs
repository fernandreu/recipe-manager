// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SearchOptionsProcessor.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the SearchOptionsProcessor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public class SearchOptionsProcessor<T, TEntity>
    {
        private readonly string[] searchQuery;

        public SearchOptionsProcessor(string[] searchQuery)
        {
            this.searchQuery = searchQuery;
        }

        public IEnumerable<SearchTerm> GetAllTerms()
        {
            if (this.searchQuery == null)
            {
                yield break;
            }

            foreach (var expression in this.searchQuery)
            {
                if (string.IsNullOrWhiteSpace(expression))
                {
                    continue;
                }

                // Each expression looks like:
                // "fieldName op value..."
                var tokens = expression.Split(' ');

                if (tokens.Length < 3)
                {
                    yield return new SearchTerm
                    {
                        ValidSyntax = false,
                        Name = tokens.Length > 0 ? tokens[0] : expression,
                    };

                    continue;
                }

                yield return new SearchTerm
                {
                    ValidSyntax = true,
                    Name = tokens[0],
                    Operator = tokens[1],
                    Value = string.Join(" ", tokens.Skip(2)),
                };
            }
        }

        public IEnumerable<SearchTerm> GetValidTerms()
        {
            var queryTerms = this.GetAllTerms()
                .Where(x => x.ValidSyntax)
                .ToArray();

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

                yield return new SearchTerm
                {
                    ValidSyntax = term.ValidSyntax,
                    Name = declaredTerm.Name,
                    Operator = term.Operator,
                    Value = term.Value,
                    ExpressionProvider = declaredTerm.ExpressionProvider,
                };
            }
        }
        
        public IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            var terms = this.GetValidTerms().ToArray();
            if (!terms.Any())
            {
                return query;
            }

            var modifiedQuery = query;

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<TEntity>(term.Name);
                var obj = ExpressionHelper.Parameter<TEntity>();

                // Build up the LINQ expression backwards
                // query = query.Where(x => x.Property == "Value");

                // x.Property
                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);

                // "Value"
                var right = term.ExpressionProvider.GetValue(term.Value);

                // x.Property == "Value"
                var comparisonExpression = term.ExpressionProvider.GetComparison(left, term.Operator, right);

                // x => x.Property == "Value"
                var lambdaExpression = ExpressionHelper.GetLambda<TEntity, bool>(obj, comparisonExpression);

                // query = query.Where(...)
                modifiedQuery = ExpressionHelper.CallWhere(modifiedQuery, lambdaExpression);
            }

            return modifiedQuery;
        }

        private static IEnumerable<SearchTerm> GetTermsFromModel()
        {
            return typeof(T).GetTypeInfo()
                .DeclaredProperties
                .Where(p => p.GetCustomAttributes<SearchableAttribute>().Any())
                .Select(p => new SearchTerm
                {
                    Name = p.Name,
                    ExpressionProvider = p.GetCustomAttribute<SearchableAttribute>().ExpressionProvider,
                });
        }
    }
}
