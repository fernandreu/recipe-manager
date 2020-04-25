// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DbOptions.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 22/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.Infrastructure.Helpers
{
    public class DbOptions
    {
        public string? Type { get; set; }
        
        public bool Migrate { get; set; }
        
        public bool Create { get; set; }
        
        public bool Delete { get; set; }
    }
}