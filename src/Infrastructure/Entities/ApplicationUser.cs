using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser<Guid>, ISingleEntity
    {
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Compromise solution for EFCore")]
        public ICollection<UserIngredient>? Ingredients { get; set; }
    }
}
