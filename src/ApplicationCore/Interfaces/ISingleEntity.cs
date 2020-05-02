// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISingleEntity.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 02/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace RecipeManager.ApplicationCore.Interfaces
{
    public interface ISingleEntity
    {
        Guid Id { get; set; }
    }
}