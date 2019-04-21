namespace RecipeManager.ApplicationCore.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public abstract class IncludeAttribute : Attribute
    {
    }
}
