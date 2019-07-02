﻿using System.Collections.Generic;

namespace RecipeManager.WebApi.Resources
{
    public class RecipeResource : BaseResource
    {
        public string Title { get; set; }
        
        public ICollection<IngredientResource> Ingredients { get; set; }

        public string Details { get; set; }
    }
}