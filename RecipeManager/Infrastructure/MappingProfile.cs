// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MappingProfile.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Defines the MappingProfile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure
{
    using AutoMapper;

    using RecipeManager.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<RecipeEntity, Recipe>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RecipesController.GetRecipeById), new { recipeId = src.Id })));
            this.CreateMap<IngredientEntity, Ingredient>();
        }
    }
}
