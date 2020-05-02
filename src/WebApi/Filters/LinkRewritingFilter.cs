using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using RecipeManager.ApplicationCore.Extensions;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.WebApi.Helpers;

namespace RecipeManager.WebApi.Filters
{
    internal class LinkRewritingFilter : IAsyncResultFilter
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        public LinkRewritingFilter(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var asObjectResult = context.Result as ObjectResult;
            var shouldSkip = asObjectResult == null || asObjectResult.StatusCode >= 400 || !(asObjectResult.Value is BaseResource);

            if (shouldSkip)
            {
                return next();
            }

            var rewriter = new LinkRewriter(urlHelperFactory.GetUrlHelper(context));
            RewriteAllLinks(asObjectResult!.Value, rewriter);

            return next();
        }

        private static void RewriteAllLinks(object? model, LinkRewriter rewriter)
        {
            if (model == null)
            {
                return;
            }

            var allProperties = model.GetType().GetTypeInfo().GetAllProperties().Where(p => p.CanRead).ToArray();

            var linkProperties = allProperties.Where(p => p.CanWrite && p.PropertyType == typeof(Link)).ToList();

            foreach (var linkProperty in linkProperties)
            {
                var rewritten = rewriter.Rewrite(linkProperty.GetValue(model) as Link);
                if (rewritten == null)
                {
                    continue;
                }

                linkProperty.SetValue(model, rewritten);

                if (linkProperty.Name != nameof(BaseResource.Self))
                {
                    continue;
                }

                // Special handling of the hidden Self property: unwrap into the root object
                allProperties.SingleOrDefault(p => p.Name == nameof(BaseResource.Href))?.SetValue(model, rewritten.Href);
                allProperties.SingleOrDefault(p => p.Name == nameof(BaseResource.Method))?.SetValue(model, rewritten.Method);
                allProperties.SingleOrDefault(p => p.Name == nameof(BaseResource.Relations))?.SetValue(model, rewritten.Relations);
            }

            var arrayProperties = allProperties.Where(p => typeof(IEnumerable<object>).IsAssignableFrom(p.PropertyType)).ToList();
            RewriteLinksInLists(arrayProperties, model, rewriter);

            var objectProperties = allProperties.Except(linkProperties).Except(arrayProperties);
            RewriteLinksInNestedObjects(objectProperties, model, rewriter);
        }

        private static void RewriteLinksInNestedObjects(IEnumerable<PropertyInfo> objectProperties, object model, LinkRewriter rewriter)
        {
            foreach (var objectProperty in objectProperties)
            {
                if (objectProperty.PropertyType == typeof(string) || objectProperty.PropertyType == typeof(char))
                {
                    continue;
                }

                var typeInfo = objectProperty.PropertyType.GetTypeInfo();
                if (typeInfo.IsClass)
                {
                    RewriteAllLinks(objectProperty.GetValue(model), rewriter);
                }
            }
        }

        private static void RewriteLinksInLists(IEnumerable<PropertyInfo> arrayProperties, object model, LinkRewriter rewriter)
        {

            foreach (var arrayProperty in arrayProperties)
            {
                var array = arrayProperty.GetValue(model) as IEnumerable<object> ?? Enumerable.Empty<object>();

                foreach (var element in array)
                {
                    RewriteAllLinks(element, rewriter);
                }
            }
        }
    }
}
