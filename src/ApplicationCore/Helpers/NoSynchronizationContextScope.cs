// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoSynchronizationContextScope.cs" company="MasterChefs">
//   {{Copyright}}
// </copyright>
// <summary>
//   Created by Fernando Andreu on 03/05/2020.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading;

namespace RecipeManager.ApplicationCore.Helpers
{
    public static class NoSynchronizationContextScope
    {
        public static IDisposable Enter()
        {
            var context = SynchronizationContext.Current;
            SynchronizationContext.SetSynchronizationContext(null);
            return new Disposable(context);
        }

        private readonly struct Disposable : IDisposable
        {
            private readonly SynchronizationContext synchronizationContext;

            public Disposable(SynchronizationContext synchronizationContext)
            {
                this.synchronizationContext = synchronizationContext;
            }

            public void Dispose() => SynchronizationContext.SetSynchronizationContext(synchronizationContext);
        }
    }
}