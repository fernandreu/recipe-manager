using System.Linq.Expressions;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using RecipeManager.ApplicationCore.Attributes;
    using RecipeManager.ApplicationCore.Entities;
    using RecipeManager.ApplicationCore.Helpers;

    public class SearchOptionsProcessor<T> where T : BaseEntity
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
        
        public void Apply(ISpecification<T> spec)
        {
            // TODO: Split these between client and server criteria depending on the type of expression
            spec.ServerCriteria.Clear();
            spec.ClientCriteria.Clear();
            
            var terms = this.GetValidTerms().ToArray();
            if (!terms.Any())
            {
                return;
            }

            foreach (var term in terms)
            {
                var propertyInfo = ExpressionHelper.GetPropertyInfo<T>(term.Name);
                var obj = ExpressionHelper.Parameter<T>();

                // Build up the LINQ expression backwards
                // x => x.Property == "Value";

                // x.Property
                var left = ExpressionHelper.GetPropertyExpression(obj, propertyInfo);

                // "Value"
                var right = term.ExpressionProvider.GetValue(term.Value);

                // x.Property == "Value"
                var comparisonExpression = term.ExpressionProvider.Evaluate(left, term.Operator, right);

                // x => x.Property == "Value"

                if (comparisonExpression.ServerSide != null)
                {
                    var lambdaExpression = ExpressionHelper.GetLambda<T, bool>(obj, comparisonExpression.ServerSide) as Expression<Func<T, bool>>;
                    spec.ServerCriteria.Add(lambdaExpression);
                }

                if (comparisonExpression.ClientSide != null)
                {
                    var lambdaExpression = ExpressionHelper.GetLambda<T, bool>(obj, comparisonExpression.ClientSide) as Expression<Func<T, bool>>;
                    spec.ClientCriteria.Add(lambdaExpression);
                }
            }
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
