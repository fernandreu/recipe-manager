﻿using System;
using System.Linq.Expressions;

namespace RecipeManager.ApplicationCore.Specifications
{
    public class OrderByClause<T>
    {
        public OrderByClause(Expression<Func<T, object>> expression, bool descending = false)
        {
            this.Expression = expression;
            this.Descending = descending;
        }

        public Expression<Func<T, object>> Expression { get; }

        public bool Descending { get; }

    }
}
