﻿using System;

namespace RecipeManager.ApplicationCore.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Custom implementation of the Contains method for .NET Standard 2.0
        /// </summary>
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source != null && toCheck != null && source.IndexOf(toCheck, comp) >= 0;
        }
    }
}
