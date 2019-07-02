using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.WebApi.Resources;

namespace RecipeManager.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Recipe, RecipeResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RecipesController.GetRecipeById), new { recipeId = src.Id })));
            this.CreateMap<Ingredient, IngredientResource>();
        }
    }
}
