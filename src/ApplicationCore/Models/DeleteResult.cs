// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteResult.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 30/04/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace RecipeManager.ApplicationCore.Models
{
    public class DeleteResult
    {
        public bool Successful { get; set; }
        
        public int DeleteCount { get; set; }
    }
}