// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinkRewriter.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the LinkRewriter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Web.Infrastructure
{
    using Microsoft.AspNetCore.Mvc;

    using RecipeManager.Web.Models;

    public class LinkRewriter
    {
        private readonly IUrlHelper urlHelper;

        public LinkRewriter(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public Link Rewrite(Link original)
        {
            if (original == null)
            {
                return null;
            }

            return new Link
            {
                Href = this.urlHelper.Link(original.RouteName, original.RouteValues),
                Method = original.Method,
                Relations = original.Relations,
            };
        }
    }
}
