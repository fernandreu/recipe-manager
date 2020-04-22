// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmptyDataSeeder.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeManager.ApplicationCore.Entities;
using RecipeManager.ApplicationCore.Helpers;

namespace RecipeManager.Infrastructure.Seeding
{
    public abstract class EmptyDataSeeder : IDataSeeder
    {
        private readonly Random random = new Random(11223344);
        
        private ModelBuilder? modelBuilder;

        private IDictionary<Type, ICollection<SingleEntity>>? cache;
        
        public void SeedData(ModelBuilder builder)
        {
            modelBuilder = builder;
            cache = new Dictionary<Type, ICollection<SingleEntity>>();
            Seed();
        }

        protected abstract void Seed();
        
        private Guid CreateGuid()
        {
            var bytes = new byte[32];
            random.NextBytes(bytes);
            return GuidUtility.Create(GuidUtility.IsoOidNamespace, bytes);
        }
        
        protected IEnumerable<T> Get<T>()
            where T : SingleEntity
        {
            if (cache == null)
            {
                throw new InvalidOperationException($"The {cache} was not initialized. This needs to occur via a {nameof(SeedData)} call");
            }

            if (!cache.TryGetValue(typeof(T), out var collection))
            {
                return Enumerable.Empty<T>();
            }

            return collection.OfType<T>();
        }
        
        protected void Add<T>(IEnumerable<T> enumerable)
            where T : SingleEntity
        {
            Add(enumerable.ToArray());
        }

        protected void Add<T>(params T[] entities)
            where T : SingleEntity
        {
            if (modelBuilder == null || cache == null)
            {
                throw new InvalidOperationException($"The {modelBuilder} or the {cache} were not initialized. This needs to occur via a {nameof(SeedData)} call");
            }
            
            if (!cache.TryGetValue(typeof(T), out var collection))
            {
                collection = new List<SingleEntity>();
                cache[typeof(T)] = collection;
            }
            
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    continue;
                }
            
                if (entity.Id == default)
                {
                    entity.Id = CreateGuid();
                }
            
                modelBuilder.Entity<T>()
                    .HasData(entity);

                collection.Add(entity);
            }
        }
        
        protected void Add(params Recipe[] recipes)
        {
            foreach (var recipe in recipes)
            {
                var ingredients = recipe.Ingredients;
                recipe.Ingredients = null;
                Add<Recipe>(recipe);

                if (ingredients == null)
                {
                    continue;
                }

                foreach (var ingredient in ingredients)
                {
                    ingredient.RecipeId = recipe.Id;
                }
                
                Add(ingredients);
            }
        }
        
        protected void Add(params User[] users)
        {
            foreach (var user in users)
            {
                var ingredients = user.Ingredients;
                user.Ingredients = null;
                Add<User>(user);

                if (ingredients == null)
                {
                    continue;
                }

                foreach (var ingredient in ingredients)
                {
                    ingredient.UserId = user.Id;
                }
                
                Add(ingredients);
            }
        }
    }
}