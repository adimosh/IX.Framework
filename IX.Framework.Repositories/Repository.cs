using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IX.Framework.Repositories
{
    /// <summary>
    /// A standard repository implementation.
    /// </summary>
    public abstract class Repository<TDataContract> : IDisposable
        where TDataContract : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Repository"/> class.
        /// </summary>
        protected Repository()
        {
        }

        /// <summary>
        /// Selects all data from the repository. This method can be overridden.
        /// </summary>
        /// <returns>An enumerable of the data contracts present in the repository.</returns>
        public virtual IEnumerable<TDataContract> Select()
        {
            return Select(null);
        }

        /// <summary>
        /// Selects data from the repository according to a specific criteria. This method can be overridden.
        /// </summary>
        /// <param name="whereFunc">The filtration function to apply to the repository.</param>
        /// <returns>An enumerable of the data contracts present in the repository.</returns>
        public virtual IEnumerable<TDataContract> Select(Func<IQueryable<TDataContract>, IQueryable<TDataContract>> whereFunc)
        {
            IQueryable<TDataContract> query = BeginQuery();

            if (whereFunc != null) query = whereFunc(query);
            
            return query;
        }

        public virtual async Task<IEnumerable<TDataContract>> SelectAsync(Func<IQueryable<TDataContract>, IQueryable<TDataContract>> whereFunc, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Yield();
            
            cancellationToken.ThrowIfCancellationRequested();            
            
            return Select(whereFunc);
        }

        protected abstract IQueryable<TDataContract> BeginQuery();

        public abstract void Add(TDataContract obj);

        public abstract void Remove(TDataContract obj);

        public virtual async Task AddAsync(TDataContract obj, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            Add(obj);
        }

        public virtual async Task RemoveAsync(TDataContract obj, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            Remove(obj);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Repository()
        {
            Dispose(false);
        }

        private void Dispose(bool isRegular)
        {
            if (isRegular)
                SyncChanges();
        }

        public abstract void SyncChanges();

        public virtual async Task SyncChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.Yield();

            cancellationToken.ThrowIfCancellationRequested();

            SyncChanges();
        }
    }
}