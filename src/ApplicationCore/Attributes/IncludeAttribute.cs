namespace RecipeManager.ApplicationCore.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class IncludeAttribute : Attribute
    {
    }
}
