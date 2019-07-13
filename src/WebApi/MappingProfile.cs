using AutoMapper;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Resources;

namespace RecipeManager.WebApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Recipe, RecipeResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RecipesController.GetRecipeById), new { recipeId = src.Id })))
                .ReverseMap();

            this.CreateMap<Ingredient, IngredientResource>()
                .ReverseMap();

            this.CreateMap<User, UserResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.UsersController.GetUserById), new { userId = src.Id })))
                .ReverseMap();
        }
    }
}
