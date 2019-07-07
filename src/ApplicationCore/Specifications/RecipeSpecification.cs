using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Paging;
using RecipeManager.ApplicationCore.Search;
using RecipeManager.ApplicationCore.Sort;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class RecipeSpecification : BaseSpecification<Recipe>
    {
        public RecipeSpecification(PagingOptions pagingOptions, SearchOptions<Recipe> searchOptions, SortOptions<Recipe> sortOptions)
        {
            this.AddInclude(x => x.Ingredients);
            this.ApplyPaging(pagingOptions.Offset ?? 0, pagingOptions.Limit ?? int.MaxValue);
            this.ApplySearchOptions(searchOptions);
            this.ApplySortOptions(sortOptions);
        }
    }
}
