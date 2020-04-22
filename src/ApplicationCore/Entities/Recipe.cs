﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RecipeManager.ApplicationCore.Attributes;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.ApplicationCore.Entities
{
    public class Recipe : SingleEntity, IRecipe<RecipeIngredient>
    {
        [Sortable(Default = true)]
        [Searchable]
        public string Title { get; set; } = string.Empty;
        
        [IncludeInAllQueries]
        [SearchableIngredients]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Compromise solution for EFCore")]
        public ICollection<RecipeIngredient>? Ingredients { get; set; }
        
        [Sortable]
        [Searchable]
        public string? Details { get; set; }
    }
}
