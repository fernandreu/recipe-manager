using System;
using RecipeManager.ApplicationCore.Interfaces;

namespace RecipeManager.Infrastructure.Entities
{
    public class SingleEntity : ISingleEntity
    {
        public Guid Id { get; set; }
    }
}
