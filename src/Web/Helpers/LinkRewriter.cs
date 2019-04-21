namespace RecipeManager.Web.Helpers
{
    using Microsoft.AspNetCore.Mvc;

    using RecipeManager.Web.Resources;

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
