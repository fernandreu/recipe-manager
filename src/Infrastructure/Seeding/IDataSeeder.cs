// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDataSeeder.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;

namespace RecipeManager.Infrastructure.Seeding
{
    public interface IDataSeeder
    {
        void SeedData(ModelBuilder builder);
    }
}