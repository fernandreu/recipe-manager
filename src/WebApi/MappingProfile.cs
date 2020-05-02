using AutoMapper;
using RecipeManager.ApplicationCore.Resources;
using RecipeManager.ApplicationCore.Specifications;
using RecipeManager.Infrastructure.Entities;

namespace RecipeManager.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Recipe, RecipeResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RecipesController.GetRecipeById), new { recipeId = src.Id })))
                .ReverseMap();

            CreateMap<RecipeIngredient, IngredientResource>()
                .ReverseMap();

            CreateMap<UserIngredient, IngredientResource>()
                .ReverseMap();
            
            CreateMap<ApplicationUser, UserResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.UsersController.GetUserById), new { userId = src.Id })))
                .ReverseMap();
            
            // The following are needed to map a Specification<TResource> to a Specification<TEntity>
            CreateMap(typeof(Specification<>), typeof(Specification<>));
            CreateMap(typeof(OrderByClause<>), typeof(OrderByClause<>));
        }
    }
}
